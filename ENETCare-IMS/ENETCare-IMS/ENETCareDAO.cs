using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
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
                SetUpDistricts(sqlLink);
                Clients = LoadClients(sqlLink);
                SetUpUsers(sqlLink);
                SetUpInterventionTypes(sqlLink);
                Interventions = LoadInterventions(sqlLink);

            }
        }

        private string GetConnectionString()
        {
            return ConfigurationManager
                .ConnectionStrings["DatabaseConnection"]
                .ConnectionString;
        }

        private void SetUpInterventionTypes(SqlConnection sql)
        {
            InterventionTypes = new InterventionTypes();

            LoadInterventionTypes(sql);

            //Only for our purposes - we need to have existing InterventionTypes in the system.
            if (InterventionTypes.Count == 0)
            {
                InterventionTypes.PopulateTypes();
                for (int i = 0; i < InterventionTypes.Count; i++)
                {
                    Save(InterventionTypes[i]);
                }
            }
        }

        private void SetUpDistricts(SqlConnection sql)
        {
            Districts = new Districts(this);

            LoadDistricts(sql);

            //Only for our purposes - we need to have existing InterventionTypes in the system.
            if (Districts.Count == 0)
            {
                Districts.PopulateDistricts();
                for (int i = 0; i < Districts.Count; i++)
                {
                    Save(Districts[i]);
                }
            }
        }

        private void LoadInterventionTypes(SqlConnection sql)
        {
            SqlCommand query = new SqlCommand(
                String.Format("SELECT * FROM {0}", DatabaseConstants.INTERVENTIONTYPES_NAME),
                sql);

            SqlDataAdapter adapter = new SqlDataAdapter(query);

            EnetCareImsDataSet dataSet = new EnetCareImsDataSet();
            adapter.Fill(dataSet, DatabaseConstants.INTERVENTIONTYPES_NAME);

            foreach (EnetCareImsDataSet.InterventionTypesRow typesRow in dataSet.InterventionTypes)
            {
                InterventionType type = new InterventionType(typesRow.InterventionTypeId, typesRow.Name, typesRow.Cost, (decimal)typesRow.Labour);
                InterventionTypes.Add(type);
            }
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


            IMS.Interventions.Interventions interventions = new IMS.Interventions.Interventions(this);
            foreach (EnetCareImsDataSet.InterventionsRow interventionRow in dataSet.Interventions)
            {
                int id = interventionRow.InterventionId;
                InterventionType type = InterventionTypes.GetTypeByID(interventionRow.InterventionTypeId);
                Client client = Clients.GetClientByID(interventionRow.ClientId);
                SiteEngineer user = (SiteEngineer)Users.GetUserByID(interventionRow.ProposingEngineerId);
                DateTime date = interventionRow.Date;
                decimal labour = (decimal)interventionRow.Labour;
                decimal cost = interventionRow.Cost;
                string notes = interventionRow.Notes;

                Intervention intervention = Intervention.Factory.CreateIntervention(id, type, client, user, labour, cost, date);
                intervention.UpdateNotes(user, notes);

                interventions.Add(intervention);
            }

            return interventions;
        }

        public void SetUpUsers(SqlConnection sql)
        {
            Users = new IMS.Users.Users(this);

            LoadUsers(sql);

            //Only for our purposes - we need to have existing InterventionTypes in the system.
            if (Users.Count == 0)
            {
                Users.PopulateUsersList();
                for (int i = 1; i < Users.Count; i++)
                {
                    Save(Users[i]);
                }
            }
        }

        public void LoadUsers(SqlConnection sql)
        {
            #region A Right Shambles
            SqlCommand userQuery = new SqlCommand(
                String.Format("SELECT * FROM {0}", DatabaseConstants.USERS_TABLE_NAME),
                sql);

            SqlCommand siteManagerQuery = new SqlCommand(
                String.Format("SELECT * FROM {0}", DatabaseConstants.SITE_ENGINEERS_TABLE_NAME),
                sql);

            SqlCommand managerQuery = new SqlCommand(
                String.Format("SELECT * FROM {0}", DatabaseConstants.MANAGERS_TABLE_NAME),
                sql);

            SqlDataAdapter adapter = new SqlDataAdapter(userQuery);
            EnetCareImsDataSet userDataSet = new EnetCareImsDataSet();
            adapter.Fill(userDataSet, DatabaseConstants.USERS_TABLE_NAME);

            adapter = new SqlDataAdapter(siteManagerQuery);
            EnetCareImsDataSet siteEngineerDataSet = new EnetCareImsDataSet();
            adapter.Fill(siteEngineerDataSet, DatabaseConstants.SITE_ENGINEERS_TABLE_NAME);

            adapter = new SqlDataAdapter(managerQuery);
            EnetCareImsDataSet managerDataSet = new EnetCareImsDataSet();
            adapter.Fill(managerDataSet, DatabaseConstants.MANAGERS_TABLE_NAME);


            foreach (EnetCareImsDataSet.Users_SiteEngineersRow siteEngineerRow in siteEngineerDataSet.Users_SiteEngineers.Rows)// .Users_Managers)
            {
                int id = siteEngineerRow.UserId;
                District district = Districts.GetDistrictByID(siteEngineerRow.DistrictId);
                decimal labour = (decimal)siteEngineerRow.MaxApprovableLabour;
                decimal cost = siteEngineerRow.MaxApprovableCost;

                #region Code To Revise
                //string name = ((EnetCareImsDataSet.UsersRow)userDataSet.Users.Rows[id]).Name;
                //string username = joinTableRow.Username;
                //string password = joinTableRow.Password;
                //string plainText = joinTableRow.PlaintextPassword;

                //Manager manager = new Manager(id, name, username, plainText, district, labour, cost);
                //Users.Add(manager);
                #endregion
            }

            #region Code That's Probably Unnecessary
            //Do same for SiteEngineers
            //query = new SqlCommand(
            //    DatabaseConstants.USERS_SITEENGINEERS_JOIN_SQL,
            //    sql);

            //dataSet = new EnetCareImsDataSet();
            //adapter.Fill(dataSet, DatabaseConstants.USERS_JOIN_TABLE_NAME);

            //foreach (EnetCareImsDataSet.UserJoinTableRow joinTableRow in dataSet.UserJoinTable)// .Users_Managers)
            //{
            //    int id = joinTableRow.UserId;
            //    District district = Districts.GetDistrictByID(joinTableRow.DistrictId);
            //    decimal labour = (decimal)joinTableRow.MaxApprovableLabour;
            //    decimal cost = joinTableRow.MaxApprovableCost;
            //    string name = joinTableRow.Name;
            //    string username = joinTableRow.Username;
            //    string password = joinTableRow.Password;
            //    string plainText = joinTableRow.PlaintextPassword;

            //    SiteEngineer engineer = new SiteEngineer(id, name, username, plainText, district, labour, cost);
            //    Users.Add(engineer);
            //}
            #endregion
            #endregion
        }

        public void LoadDistricts(SqlConnection sql)
        {
            SqlCommand query = new SqlCommand(
                String.Format("SELECT * FROM {0}", DatabaseConstants.DISTRICTS_TABLE_NAME),
                sql);

            SqlDataAdapter adapter = new SqlDataAdapter(query);

            EnetCareImsDataSet dataSet = new EnetCareImsDataSet();
            adapter.Fill(dataSet, DatabaseConstants.DISTRICTS_TABLE_NAME);

            foreach (EnetCareImsDataSet.DistrictsRow districtRow in dataSet.Districts)
            {
                int id = districtRow.DistrictId;

                string name = districtRow.Name;

                District district = new District(id, name);

                Districts.Add(district);
            }
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
                sqlLink.Close();

            }
        }

    }
}
