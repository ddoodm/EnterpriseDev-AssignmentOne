using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENETCare.IMS.Interventions;
namespace ENETCare.IMS
{
    public class Clients
    {
        private ENETCareDAO application;

        private List<Client> clients;

        public Clients(ENETCareDAO application)
        {
            this.application = application;
            clients = new List<Client>();

            PopulateClients();
        }

        /// <summary>
        /// Initializes a Clients DTO with a pre-defined collection of Clients.
        /// </summary>
        private Clients(ENETCareDAO application, List<Client> clients)
        {
            this.application = application;
            this.clients = clients;
        }

        private void PopulateClients()
        {
            // TODO: This will retrieve ENETCare's clients from
            // the database. These are temporary placeholders.
            Districts districts = application.Districts;
            clients.Add(new Client(0, "John Smith", "1234 Alphabet Street",     districts.GetDistrictByID(0)));
            clients.Add(new Client(1, "John Doe", "1 Alpha Road",               districts.GetDistrictByID(1)));
            clients.Add(new Client(2, "Jane Smith", "2 Beta Lane",              districts.GetDistrictByID(2)));
            clients.Add(new Client(3, "Jane Doe", "3 Gamma Plaza",              districts.GetDistrictByID(1)));
            clients.Add(new Client(4, "John Doe Smith", "4 Delta Place",        districts.GetDistrictByID(2)));
            clients.Add(new Client(5, "Jane Doe Smith", "5 Epsilon Boulevarde", districts.GetDistrictByID(1)));
        }

        public Client GetClientByID(int id)
        {
            return clients.First<Client>(c => c.ID == id);
        }

        public Clients FilterByName(string name)
        {
            name = name.ToLower();
            var results =
                from client in clients
                where client.Name.ToLower().Contains(name)
                select client;
            return new Clients(application, results.ToList<Client>());
        }

        public Clients FilterByDistrict(District district)
        {
            var results =
                from client in clients
                where client.District == district
                select client;
            return new Clients(application, results.ToList<Client>());
        }

        /// <summary>
        /// Computes the next available ID number
        /// </summary>
        private int NextID
        {
            get
            {
                if (clients.Count < 1)
                    return 0;

                var highestClient
                    = clients.OrderByDescending(i => i.ID)
                    .FirstOrDefault();
                return highestClient.ID + 1;
            }
        }

        private void Add(Client client)
        {
            clients.Add(client);
        }

        public Client CreateClient(string name, string location, District district)
        {
            int id = NextID;
            Client newClient = new Client(id, name, location, district);
            Add(newClient);
            return newClient;
        }

        public List<Client> CopyAsList()
        {
            return new List<Client>(clients);
        }
    }
}
