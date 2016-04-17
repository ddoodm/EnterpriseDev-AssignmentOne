using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENETCare.IMS.Interventions;
namespace ENETCare.IMS
{
    public static class Clients
    {
        private static List<Client> clients = new List<Client>();

        public static void PopulateClients()
        {
            // TODO: This will retrieve ENETCare's clients from
            // the database. These are temporary placeholders.
            if (clients.Count == 0)
            {
                clients.Add(new Client(0, "John Smith", "1234 Alphabet Street", 0));
                clients.Add(new Client(1, "John Doe", "1 Alpha Road", 1));
                clients.Add(new Client(2, "Jane Smith", "2 Beta Lane", 2));
                clients.Add(new Client(3, "Jane Doe", "3 Gamma Plaza", 3));
                clients.Add(new Client(4, "John Doe Smith", "4 Delta Place", 4));
                clients.Add(new Client(5, "Jane Doe Smith", "5 Epsilon Boulevarde", 5));
            }
        }

        public static Client GetClientByID(int id)
        {
            return clients.First<Client>(c => c.ID == id);
        }

        public static List<Client> GetClientsByDistrict(int districtID)
        {
            return clients.Where(x => x.District.ID == districtID).ToList<Client>();
        }

        public static int GetNewClientID()
        {
            return clients.Count;
        }

        public static bool IsPopulated()
        {
            return clients.Count == 0;
        }

        public static List<Client> ClientList
        {
            get
            {
                return clients;
            }
        }

        public static void AddClient(Client client)
        {
            clients.Add(client);
        }
    }
}
