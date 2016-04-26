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

        private const string INTERVENTIONS_TABLE_NAME = "Interventions";
        private const string CLIENTS_TABLE_NAME = "Clients";

        public ENETCareDAO()
        {
            string sqlConnectionString = GetConnectionString();

            using (SqlConnection sqlLink = new SqlConnection(sqlConnectionString))
            {
                Districts = new Districts(this);
                Clients = LoadClients(sqlLink);
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

            return new IMS.Interventions.Interventions(this);
        }
    }
}
