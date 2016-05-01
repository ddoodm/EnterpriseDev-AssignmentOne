﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using ENETCare.IMS.Data;
using ENETCare.IMS.Interventions;
using ENETCare.IMS.Users;

namespace ENETCare.IMS
{
    /// <summary>
    /// Provides Data Access for ENETCare data sources
    /// </summary>
    public class ENETCareDAO
    {
        private string sqlConnectionString;

        public Interventions.Interventions Interventions { get; private set; }
        public InterventionTypes InterventionTypes { get; private set; }
        public Districts Districts { get; private set; }
        public Clients Clients { get; private set; }
        public Users.Users Users { get; private set; }

        public ENETCareDAO()
        {
            sqlConnectionString = GetConnectionString();

            using (SqlConnection sqlLink = new SqlConnection(sqlConnectionString))
            {
                this.Districts = LoadDistricts(sqlLink);
                this.Clients = LoadClients(sqlLink);
                this.Users = LoadUsers(sqlLink);
                this.InterventionTypes = LoadInterventionTypes(sqlLink);
                this.Interventions = LoadInterventions(sqlLink);
            }
        }

        public static ENETCareDAO Context
        {
            get { return new ENETCareDAO(); }
        }

        private string GetConnectionString()
        {
            return ConfigurationManager
                .ConnectionStrings["DatabaseConnection"]
                .ConnectionString;
        }

        private InterventionTypes LoadInterventionTypes(SqlConnection sql)
        {
            // Select all from table, given the table name
            SqlCommand query = new SqlCommand(
                String.Format(
                    "SELECT * FROM [dbo].[{0}]",
                    DatabaseConstants.INTERVENTIONTYPES_NAME),
                sql);
            SqlDataAdapter adapter = new SqlDataAdapter(query);

            EnetCareImsDataSet dataSet = new EnetCareImsDataSet();
            adapter.Fill(dataSet, DatabaseConstants.INTERVENTIONTYPES_NAME);

            InterventionTypes types = new InterventionTypes();

            foreach (EnetCareImsDataSet.InterventionTypesRow typesRow in dataSet.InterventionTypes)
            {
                InterventionType type = new InterventionType(
                    typesRow.InterventionTypeId,
                    typesRow.Name,
                    typesRow.Cost,
                    typesRow.Labour);
                types.Add(type);
            }

            return types;
        }

        private Clients LoadClients(SqlConnection sql)
        {
            // Select all from table, given the table name
            SqlCommand query = new SqlCommand(
                String.Format(
                    "SELECT * FROM [dbo].[{0}]",
                    DatabaseConstants.CLIENTS_TABLE_NAME),
                sql);
            SqlDataAdapter adapter = new SqlDataAdapter(query);

            EnetCareImsDataSet dataSet = new EnetCareImsDataSet();
            adapter.Fill(dataSet, DatabaseConstants.CLIENTS_TABLE_NAME);

            Clients clients = new Clients(this);

            foreach (EnetCareImsDataSet.ClientsRow clientRow in dataSet.Clients)
            {
                District district = Districts.GetDistrictByID(clientRow.DistrictId);

                Client client = new Client(
                    clientRow.ClientId, clientRow.Name, clientRow.Location, district);

                clients.Add(client);
            }

            return clients;
        }

        private Interventions.Interventions LoadInterventions(SqlConnection sql)
        {
            // Select all from table, given the table name
            SqlCommand query = new SqlCommand(
                String.Format(
                    "SELECT * FROM [dbo].[{0}]",
                    DatabaseConstants.INTERVENTIONS_TABLE_NAME),
                sql);

            SqlDataAdapter adapter = new SqlDataAdapter(query);

            EnetCareImsDataSet dataSet = new EnetCareImsDataSet();
            adapter.Fill(dataSet, DatabaseConstants.INTERVENTIONS_TABLE_NAME);

            Interventions.Interventions interventions = new Interventions.Interventions(this);

            foreach (EnetCareImsDataSet.InterventionsRow row in dataSet.Interventions)
            {
                InterventionType type = InterventionTypes[row.InterventionTypeId];
                Client client = Clients.GetClientByID(row.ClientId);
                User siteEngineer = Users.GetUserByID(row.ProposingEngineerId);

                if (!(siteEngineer is SiteEngineer))
                    throw new InvalidDataException(
                        String.Format("Database load error\n\nIntervention #{0} references a user who is not a Site Engineer (UserID: {1}).",
                        row.InterventionId, row.ProposingEngineerId));

                // Intervention Factory will populate with type defaults if database values are null
                decimal? labour = row.IsLabourNull() ? (decimal?)null : row.Labour;
                decimal? cost = row.IsCostNull() ? (decimal?)null : row.Cost;

                // Avoids DBNull exception
                string notes = row.IsNotesNull() ? "" : row.Notes;

                // TODO: Handle Intervention Approval and Quality Management
                interventions.Add(
                    Intervention.Factory.CreateIntervention(
                        row.InterventionId, type, client, (SiteEngineer)siteEngineer,
                        labour, cost, row.Date, notes, null, null
                        ));
            }

            return interventions;
        }

        public Users.Users LoadUsers(SqlConnection sql)
        {
            Users.Users users = new Users.Users(this);
            users.Add(LoadSiteEngineers(sql));
            users.Add(LoadManagers(sql));
            users.Add(LoadAccountants(sql));
            return users;
        }

        /// <summary>
        /// Loads a collection of users of a given sub-class of User.
        /// 
        /// For example: this function may be used to load all Site Engineer users
        /// into a Users collection, when the 'tableName' is the name of the
        /// TPT (Table-Per-Type) sub-type table (ie, "Users_SiteEngineers"),
        /// and "InstantiateUser" is populated with code which returns a
        /// SiteEngineer given a row of said table.
        /// </summary>
        /// <see cref="ENETCare.IMS.ENETCareDAO.LoadSiteEngineers(SqlConnection)"/>
        /// <param name="sql">An SQL connection (initialized)</param>
        /// <param name="tableName">The name of the TPT sub-type table (ie, "Users_SiteEngineers")</param>
        /// <param name="instantiateUser">
        /// A delegate which is passed a DataRow from an inner-join of the specified
        /// sub-type table and its respective Users table row, and should return
        /// an instantiated user of the given type.
        /// </param>
        /// <returns>A Users collection, populated with all users of the specified type.</returns>
        public Users.Users LoadUsers(SqlConnection sql, string tableName, Func<DataRow, User> instantiateUser)
        {
            // Joins Table-Per-Type sub-class with its base class
            SqlCommand query = new SqlCommand(
                String.Format(
                    "SELECT * " +
                    "FROM   [dbo].[{0}] " +
                    "   INNER JOIN [dbo].[Users] " +
                    "       ON [dbo].[{0}].[UserId] = [dbo].[Users].[UserId]",
                    tableName),
                sql);

            SqlDataAdapter adapter = new SqlDataAdapter(query);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);

            // The query returns one table
            if (dataSet.Tables.Count != 1)
                throw new InvalidDataException("An error occurred when joining database tables.");
            DataTable userTable = dataSet.Tables[0];

            // Read rows into ENETCare business objects
            // TODO: Handle passwords
            Users.Users users = new Users.Users(this);
            foreach (DataRow userRow in userTable.Rows)
                users.Add(instantiateUser(userRow));

            return users;
        }

        public Users.Users LoadSiteEngineers(SqlConnection sql)
        {
            return LoadUsers(sql, "Users_SiteEngineers", row =>
            {
                // Find the Site Engineer's district
                District district = Districts.GetDistrictByID((int)row["DistrictId"]);

                // Create the Site Engineer from table data
                return new SiteEngineer (
                    (int)row["UserId"], (string)row["Name"], (string)row["Username"],
                    "1234", district, (decimal)row["MaxApprovableLabour"], (decimal)row["MaxApprovableCost"]);
            });
        }

        public Users.Users LoadManagers(SqlConnection sql)
        {
            return LoadUsers(sql, "Users_Managers", row =>
            {
                // Find the Manager's district
                District district = Districts.GetDistrictByID((int)row["DistrictId"]);

                // Create the Manager from table data
                return new Manager(
                    (int)row["UserId"], (string)row["Name"], (string)row["Username"],
                    "1234", district, (decimal)row["MaxApprovableLabour"], (decimal)row["MaxApprovableCost"]);
            });
        }

        public Users.Users LoadAccountants(SqlConnection sql)
        {
            return LoadUsers(sql, "Users_Accountants", row =>
            {
                // Create the Accountant from table data
                return new Accountant(
                    (int)row["UserId"], (string)row["Name"], (string)row["Username"], "1234");
            });
        }

        public Districts LoadDistricts(SqlConnection sql)
        {
            // Select all from table, given the table name
            SqlCommand query = new SqlCommand(
                String.Format(
                    "SELECT * FROM [dbo].[{0}]",
                    DatabaseConstants.DISTRICTS_TABLE_NAME),
                sql);

            SqlDataAdapter adapter = new SqlDataAdapter(query);
            EnetCareImsDataSet dataSet = new EnetCareImsDataSet();
            adapter.Fill(dataSet, DatabaseConstants.DISTRICTS_TABLE_NAME);

            Districts districts = new Districts(this);

            foreach (EnetCareImsDataSet.DistrictsRow districtRow in dataSet.Districts)
            {
                int id = districtRow.DistrictId;
                string name = districtRow.Name;
                District district = new District(id, name);
                districts.Add(district);
            }

            return districts;
        }

        public void Save(Intervention intervention)
        {
            using (SqlConnection sqlLink = new SqlConnection(GetConnectionString()))
            {
                sqlLink.Open();

                // Create query, and substitute table and column names first
                string queryString = String.Format(
                    "INSERT INTO {0} ({1}) VALUES(@typeId, @client, @engineer, @date, @labour, @cost, @notes);",
                    DatabaseConstants.INTERVENTIONS_TABLE_NAME,
                    DatabaseConstants.INTERVENTIONS_COLUMN_NAMES_FOR_CREATION);

                // Substitute SQL parameters using the Parameters collection
                SqlCommand query = new SqlCommand(queryString, sqlLink);
                query.Parameters.AddWithValue("@typeId",    intervention.InterventionType.ID);
                query.Parameters.AddWithValue("@client",    intervention.Client.ID);
                query.Parameters.AddWithValue("@engineer",  intervention.SiteEngineer.ID);
                query.Parameters.AddWithValue("@date",      intervention.Date);
                query.Parameters.AddWithValue("@labour",    intervention.Labour);
                query.Parameters.AddWithValue("@cost",      intervention.Cost);

                // Permit null notes
                query.Parameters.AddWithValue("@notes",
                    ((object)intervention.Notes)?? DBNull.Value);

                query.ExecuteNonQuery();
                sqlLink.Close();
            }
        }

        public void Save(Client client)
        {
            using (SqlConnection sqlLink = new SqlConnection(GetConnectionString()))
            {
                sqlLink.Open();

                // Create query, and substitute table and column names first
                string queryString = String.Format(
                    "INSERT INTO {0} ({1}) VALUES(@name, @location, @district);",
                    DatabaseConstants.CLIENTS_TABLE_NAME,
                    DatabaseConstants.CLIENTS_COLUMN_NAMES_FOR_CREATION);

                // Substitute SQL parameters using the Parameters collection
                SqlCommand query = new SqlCommand(queryString, sqlLink);
                query.Parameters.AddWithValue("@name",      client.Name);
                query.Parameters.AddWithValue("@location",  client.Location);
                query.Parameters.AddWithValue("@district",  client.District.ID);

                query.ExecuteNonQuery();
                sqlLink.Close();
            }
        }
    }
}
