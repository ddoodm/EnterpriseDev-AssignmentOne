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
        public Users.Users EnetUsers { get; private set; }

        private const string INTERVENTIONS_TABLE_NAME = "Interventions";
        private const string CLIENTS_TABLE_NAME = "Clients";

        public ENETCareDAO()
        {
            string sqlConnectionString = GetConnectionString();

            using (SqlConnection sqlLink = new SqlConnection(sqlConnectionString))
            {
                Districts = new Districts(this);
                Clients = LoadClients(sqlLink);
                Users = new Users.Users(this);
                InterventionTypes = new InterventionTypes();
                Interventions = LoadInterventions(sqlLink);
            }
        }

        private string GetConnectionString()
        {
            return ConfigurationManager
                .ConnectionStrings["DatabaseConnection"]
                .ConnectionString;
        }

        private Clients LoadClients(SqlConnection sql)
        {
            SqlCommand query = new SqlCommand(
                String.Format("SELECT * FROM {0}", CLIENTS_TABLE_NAME),
                sql);

            SqlDataAdapter adapter = new SqlDataAdapter(query);

            EnetCareImsDataSet dataSet = new EnetCareImsDataSet();
            adapter.Fill(dataSet, CLIENTS_TABLE_NAME);

            Clients clients = new Clients(this);

            foreach(EnetCareImsDataSet.ClientsRow clientRow in dataSet.Clients)
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
                String.Format("SELECT * FROM {0}", INTERVENTIONS_TABLE_NAME),
                sql);

            SqlDataAdapter adapter = new SqlDataAdapter(query);

            EnetCareImsDataSet dataSet = new EnetCareImsDataSet();
            adapter.Fill(dataSet, INTERVENTIONS_TABLE_NAME);

            Interventions.Interventions interventions = new Interventions.Interventions(this);

            foreach(EnetCareImsDataSet.InterventionsRow row in dataSet.Interventions)
            {
                InterventionType type = InterventionTypes[row.InterventionTypeId];
                Client client = Clients.GetClientByID(row.ClientId);
                User siteEngineer = EnetUsers.GetUserById(row.ProposingEngineerId);

                if (!(siteEngineer is SiteEngineer))
                    throw new InvalidDataException(
                        String.Format("Database load error\n\nIntervention #{0} references a user who is not a Site Engineer (UserID: {1}).",
                        row.InterventionId, row.ProposingEngineerId));

                interventions.Add(
                    Intervention.Factory.CreateIntervention(
                        row.InterventionId, type, client, (SiteEngineer)siteEngineer,
                        row.Labour, row.Cost, row.Date
                        ));
            }

            return interventions;
        }
    }
}
