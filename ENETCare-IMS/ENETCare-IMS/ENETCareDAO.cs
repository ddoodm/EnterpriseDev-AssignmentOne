using System;
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
        public Interventions.Interventions Interventions { get; private set; }
        public InterventionTypes InterventionTypes { get; private set; }
        public Districts Districts { get; private set; }
        public Clients Clients { get; private set; }
        public Users.Users Users { get; private set; }

        public ENETCareDAO()
        {
            string sqlConnectionString = GetConnectionString();

            using (SqlConnection sqlLink = new SqlConnection(sqlConnectionString))
            {
                Districts = LoadDistricts(sqlLink);
                Clients = LoadClients(sqlLink);
                Users = LoadUsers(sqlLink);
                InterventionTypes = LoadInterventionTypes(sqlLink);
                Interventions = LoadInterventions(sqlLink);
            }
        }

        private string GetConnectionString()
        {
            return ConfigurationManager
                .ConnectionStrings["DatabaseConnection"]
                .ConnectionString;
        }

        private InterventionTypes LoadInterventionTypes(SqlConnection sql)
        {
            SqlCommand query = new SqlCommand(
                String.Format("SELECT * FROM {0}", DatabaseConstants.INTERVENTIONTYPES_NAME),
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
            SqlCommand query = new SqlCommand(
                String.Format("SELECT * FROM {0}", DatabaseConstants.CLIENTS_TABLE_NAME),
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
            SqlCommand query = new SqlCommand(
                String.Format("SELECT * FROM {0}", DatabaseConstants.INTERVENTIONS_TABLE_NAME),
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
            //users.Add(LoadManagers(sql));
            //users.Add(LoadAccountants(sql));
            return users;
        }

        public Users.Users LoadSiteEngineers(SqlConnection sql)
        {
            SqlCommand query = new SqlCommand(
                "SELECT * " +
                "FROM   [dbo].[Users_SiteEngineers] " +
                "   INNER JOIN [dbo].[Users] " +
                "       ON [dbo].[Users_SiteEngineers].[UserId] = [dbo].[Users].[UserId]"
                , sql);

            SqlDataAdapter adapter = new SqlDataAdapter(query);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);

            // The query returns one table
            if (dataSet.Tables.Count != 1)
                throw new InvalidDataException("An error occurred when joining database tables.");
            DataTable engineerTable = dataSet.Tables[0];

            // Read rows into ENETCare business objects
            // TODO: Handle passwords
            Users.Users engineers = new Users.Users(this);
            foreach(DataRow engineerRow in engineerTable.Rows)
            {
                District district = Districts.GetDistrictByID((int)engineerRow["DistrictId"]);

                SiteEngineer engineer = new SiteEngineer(
                    (int)engineerRow["UserId"], (string)engineerRow["Name"], (string)engineerRow["Username"],
                    "1234", district, (decimal)engineerRow["MaxApprovableLabour"], (decimal)engineerRow["MaxApprovableCost"]);
                engineers.Add(engineer);
            }

            return engineers;
        }

        /*
        public Users.Users LoadManagers(SqlConnection sql)
        {

        }

        public Users.Users LoadAccountants(SqlConnection sql)
        {

        }
        */
        public Districts LoadDistricts(SqlConnection sql)
        {
            SqlCommand query = new SqlCommand(
                String.Format("SELECT * FROM {0}", DatabaseConstants.DISTRICTS_TABLE_NAME),
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

                string notes = "'" + intervention.Notes + "'" ?? "'NULL'";
                string date = "'" + intervention.Date.ToString("yyyy-MM-dd HH:mm:ss") + "'";

                SqlCommand query = new SqlCommand(
                String.Format(" INSERT INTO {0} ({1}) VALUES({2}, {3}, {4}, {5}, {6}, {7}, {8});",
                    DatabaseConstants.INTERVENTIONS_TABLE_NAME,
                    DatabaseConstants.INTERVENTIONS_COLUMN_NAMES_FOR_CREATION,
                    intervention.InterventionType.ID,
                    intervention.Client.ID,
                    intervention.SiteEngineer.ID,
                    "convert(datetime, " + date + ")",
                    intervention.Labour,
                    intervention.Cost,
                    notes),
                sqlLink);

                query.ExecuteNonQuery();
                sqlLink.Close();
            }
        }

        public void Save(InterventionType type)
        {
            using (SqlConnection sqlLink = new SqlConnection(GetConnectionString()))
            {
                sqlLink.Open();

                string name = "'" + type.Name + "'" ?? "'NULL'";

                SqlCommand query = new SqlCommand(
                String.Format("INSERT INTO {0} ({1}) VALUES({2}, {3}, {4});",
                    DatabaseConstants.INTERVENTIONTYPES_NAME,
                    DatabaseConstants.INTERVENTIONTYPES_COLUMN_NAMES_FOR_CREATION,
                    name,
                    type.Labour,
                    type.Cost),
                sqlLink);

                query.ExecuteNonQuery();
                sqlLink.Close();

            }
        }

        public void Save(User user)
        {
            using (SqlConnection sqlLink = new SqlConnection(GetConnectionString()))
            {
                sqlLink.Open();

                int id = user.ID;
                string name = "'" + user.Name + "'";
                string userName = "'" + user.Username + "'";
                string password = "'" + user.PlaintextPassword + "'";
                string plainText = "'" + user.PlaintextPassword + "'";

                SqlCommand query = new SqlCommand(
                string.Format("INSERT INTO {0} ( {1} ) VALUES ( {2}, {3}, {4}, {5} );", DatabaseConstants.USERS_TABLE_NAME, DatabaseConstants.USERS_COLUMN_NAMES_FOR_CREATION, name, userName, plainText, password),
                sqlLink);

                query.ExecuteNonQuery();

                string tableName = "";

                int districtID = 0;
                decimal labor = 0, money = 0;

                if (user is SiteEngineer)
                {
                    tableName = DatabaseConstants.SITE_ENGINEERS_TABLE_NAME;
                    districtID = ((SiteEngineer)user).District.ID;
                    labor = ((SiteEngineer)user).MaxApprovableLabour;
                    money = ((SiteEngineer)user).MaxApprovableCost;
                }
                else if (user is Manager)
                {
                    tableName = DatabaseConstants.MANAGERS_TABLE_NAME;
                    districtID = ((Manager)user).District.ID;
                    labor = ((Manager)user).MaxApprovableLabour;
                    money = ((Manager)user).MaxApprovableCost;
                }

                query = new SqlCommand(
                String.Format("INSERT INTO {0} ({1}) VALUES({2}, {3}, {4});",
                    tableName,
                    DatabaseConstants.MANAGERS_AND_SITE_ENGINEERS_COLUMN_NAMES_FOR_CREATION,
                    districtID,
                    labor,
                    money),
                sqlLink);

                sqlLink.Close();

            }
        }

        public void Save(Client client)
        {
            using (SqlConnection sqlLink = new SqlConnection(GetConnectionString()))
            {
                sqlLink.Open();

                string name = "'" + client.Name + "'";
                string location = "'" + client.Location + "'";
                int districtID = client.District.ID;

                SqlCommand query = new SqlCommand(
                String.Format("INSERT INTO {0} ({1}) VALUES({2}, {3}, {4});",
                    DatabaseConstants.CLIENTS_TABLE_NAME,
                    DatabaseConstants.CLIENTS_COLUMN_NAMES_FOR_CREATION,
                    name,
                    location,
                    districtID),
                sqlLink);

                query.ExecuteNonQuery();
                sqlLink.Close();

            }
        }

        public void Save(District district)
        {
            using (SqlConnection sqlLink = new SqlConnection(GetConnectionString()))
            {
                sqlLink.Open();

                string name = "'" + district.Name + "'";


                SqlCommand query = new SqlCommand(
                String.Format("INSERT INTO {0} ({1}) VALUES({2});",
                    DatabaseConstants.DISTRICTS_TABLE_NAME,
                    DatabaseConstants.DISTRICTS_COLUMNS_FOR_CREATION,
                    name),
                sqlLink);

                query.ExecuteNonQuery();
            }
        }
    }
}
