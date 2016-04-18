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

        private static List<Intervention> interventions = new List<Intervention>();

        public Interventions(ENETCareDAO application)
        {
            this.application = application;

            interventions = new List<Intervention>();

            SetUpData();
        }

        private void SetUpData()
        {
            // Test engineer
            SiteEngineer testEngineerDistrict0 = new SiteEngineer
                ("Bill Williams", "williams.bill", "abc1234",
                application.Clients.GetClientByID(0).District, 12, 1000);

            SiteEngineer testEngineerDistrict1 = new SiteEngineer
                ("Ted Edwardson", "edwardson.ted", "abc1234",
                application.Clients.GetClientByID(1).District, 12, 1000);

            SiteEngineer testEngineerDistrict2 = new SiteEngineer
                ("Richard Dickson", "dickson.richard", "abc1234",
                application.Clients.GetClientByID(2).District, 12, 1000);

            SiteEngineer testEngineerDistrict3 = new SiteEngineer
                ("James \"Jimmy\" Jameson", "jameson.jim", "abc1234",
                application.Clients.GetClientByID(3).District, 12, 1000);

            SiteEngineer testEngineerDistrict4 = new SiteEngineer
                ("Rupert von Ochtag Gon", "octhaggon.rupert", "abc1234",
                application.Clients.GetClientByID(4).District, 12, 1000);

            SiteEngineer testEngineerDistrict5  = new SiteEngineer
                ("Dr. Byron Orpheus", "orpheus.byron", "abc1234",
                application.Clients.GetClientByID(5).District, 12, 1000);

            // Populate interventions with fake data
            InterventionTypes types = new InterventionTypes();

            // Testing only!
            CreateIntervention(
                types[0], application.Clients.GetClientByID(0), testEngineerDistrict0); //0
            CreateIntervention(
                types[1], application.Clients.GetClientByID(4), testEngineerDistrict4); //1
            CreateIntervention(
                types[0], application.Clients.GetClientByID(3), testEngineerDistrict3); //2
            CreateIntervention(
                types[2], application.Clients.GetClientByID(2), testEngineerDistrict2); //3
            CreateIntervention(
                types[1], application.Clients.GetClientByID(0), testEngineerDistrict0); //4
            CreateIntervention(
                types[0], application.Clients.GetClientByID(1), testEngineerDistrict1); //5

            CreateIntervention(
                types[0], application.Clients.GetClientByID(5), testEngineerDistrict5); //6
            CreateIntervention(
                types[1], application.Clients.GetClientByID(1), testEngineerDistrict1); //7
            CreateIntervention(
                types[0], application.Clients.GetClientByID(3), testEngineerDistrict3); //8
            CreateIntervention(
                types[2], application.Clients.GetClientByID(2), testEngineerDistrict2); //9
            CreateIntervention(
                types[1], application.Clients.GetClientByID(0), testEngineerDistrict0); //10
            CreateIntervention(
                types[0], application.Clients.GetClientByID(5), testEngineerDistrict5); //11

            //Approve a few interventions
            interventions[4].Approve(testEngineerDistrict0);
            interventions[5].Approve(testEngineerDistrict1);
            interventions[7].Approve(testEngineerDistrict1);
            interventions[10].Approve(testEngineerDistrict0);
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
    }
}
