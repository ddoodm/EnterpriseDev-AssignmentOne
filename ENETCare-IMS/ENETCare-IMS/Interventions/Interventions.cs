using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ENETCare.IMS.Users;

namespace ENETCare.IMS.Interventions
{
    public class Interventions
    {
        private ENETCareDAO application;

        private List<Intervention> interventions = new List<Intervention>();

        public Interventions(ENETCareDAO application)
        {
            this.application = application;

            interventions = new List<Intervention>();

            SetUpData();
        }

        public Interventions(ENETCareDAO application, List<Intervention> list)
        {
            this.application = application;
            this.interventions = list;
        }

        private void SetUpData()
        {
            // Test engineers
            IMS.Users.Users users = new IMS.Users.Users(application);
            SiteEngineer testEngineer1 = (SiteEngineer)users.Login("deinyon", "1234");
            SiteEngineer testEngineer2 = (SiteEngineer)users.Login("henry", "1234");

            // Populate interventions with fake data
            InterventionTypes types = new InterventionTypes();

            // Testing only!
            CreateIntervention(
                types[0], application.Clients.GetClientByID(1), testEngineer1); //0
            CreateIntervention(
                types[1], application.Clients.GetClientByID(2), testEngineer2); //1
            CreateIntervention(
                types[0], application.Clients.GetClientByID(3), testEngineer1); //2
            CreateIntervention(
                types[2], application.Clients.GetClientByID(4), testEngineer2); //3
            CreateIntervention(
                types[1], application.Clients.GetClientByID(1), testEngineer1); //4
            CreateIntervention(
                types[0], application.Clients.GetClientByID(2), testEngineer2); //5

            //Approve a few interventions
            interventions[4].Approve(testEngineer1);
            interventions[5].Approve(testEngineer2);
        }

        /// <summary>
        /// Computes the next available ID number
        /// </summary>
        private int NextID
        {
            get
            {
                if (interventions.Count < 1)
                    return 0;

                var highestIntervention
                    = interventions.OrderByDescending(i => i.ID)
                    .FirstOrDefault();
                return highestIntervention.ID + 1;
            }
        }

        public Intervention CreateIntervention(InterventionType type, Client client, SiteEngineer siteEngineer)
        {
            int id = NextID;
            Intervention newIntervention = Intervention.Factory.CreateIntervention(
                id, type, client, siteEngineer);
            Add(newIntervention);
            return newIntervention;
        }

        private void Add(Intervention intervention)
        {
            interventions.Add(intervention);
        }

        public int Count
        {
            get { return interventions.Count; }
        }

        /// <summary>
        /// Retrieves the Intervention with the given ID
        /// </summary>
        /// <param name="ID">The ID of the Intervention to retrieve</param>
        /// <returns>The Intervention with the given ID</returns>
        public Intervention this[int ID]
        {
            get
            {
                return interventions.First<Intervention>(
                    intervention => intervention.ID == ID);
            }
        }

        // TODO: Have this return an Interventions collection as a standardized "Filter" method
        public List<Intervention> GetInterventionsWithClient(Client client)
        {
            return interventions
                .Where(x => x.Client.ID == client.ID)
                .ToList<Intervention>();
        }

        public List<Intervention> FilterByDistrict(District district)
        {
            return interventions
                .Where(x => x.District == district)
                .ToList<Intervention>();
        }
    }
}
