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
                ("Bill Williams", "williams.bill", "abc1234", User.accType.SiteEngineer,
                application.Clients.GetClientByID(0).District, 12, 1000);

            SiteEngineer testEngineerDistrict1 = new SiteEngineer
                ("Ted Edwardson", "edwardson.ted", "abc1234", User.accType.SiteEngineer,
                application.Clients.GetClientByID(1).District, 12, 1000);

            SiteEngineer testEngineerDistrict2 = new SiteEngineer
                ("Richard Dickson", "dickson.richard", "abc1234", User.accType.SiteEngineer,
                application.Clients.GetClientByID(2).District, 12, 1000);

            SiteEngineer testEngineerDistrict3 = new SiteEngineer
                ("James \"Jimmy\" Jameson", "jameson.jim", "abc1234", User.accType.SiteEngineer,
                application.Clients.GetClientByID(3).District, 12, 1000);

            SiteEngineer testEngineerDistrict4 = new SiteEngineer
                ("Rupert von Ochtag Gon", "octhaggon.rupert", "abc1234", User.accType.SiteEngineer,
                application.Clients.GetClientByID(4).District, 12, 1000);

            SiteEngineer testEngineerDistrict5  = new SiteEngineer
                ("Dr. Byron Orpheus", "orpheus.byron", "abc1234", User.accType.SiteEngineer,
                application.Clients.GetClientByID(5).District, 12, 1000);

            // Populate interventions with fake data
            InterventionTypes types = new InterventionTypes();

            // Testing only!
            int id = 0;
            interventions.Add(Intervention.Factory.CreateIntervention(
                id++, types[0], application.Clients.GetClientByID(0), testEngineerDistrict0)); //0
            interventions.Add(Intervention.Factory.CreateIntervention(
                id++, types[1], application.Clients.GetClientByID(4), testEngineerDistrict4)); //1
            interventions.Add(Intervention.Factory.CreateIntervention(
                id++, types[0], application.Clients.GetClientByID(3), testEngineerDistrict3)); //2
            interventions.Add(Intervention.Factory.CreateIntervention(
                id++, types[2], application.Clients.GetClientByID(2), testEngineerDistrict2)); //3
            interventions.Add(Intervention.Factory.CreateIntervention(
                id++, types[1], application.Clients.GetClientByID(0), testEngineerDistrict0)); //4
            interventions.Add(Intervention.Factory.CreateIntervention(
                id++, types[0], application.Clients.GetClientByID(1), testEngineerDistrict1)); //5

            interventions.Add(Intervention.Factory.CreateIntervention(
                id++, types[0], application.Clients.GetClientByID(5), testEngineerDistrict5)); //6
            interventions.Add(Intervention.Factory.CreateIntervention(
                id++, types[1], application.Clients.GetClientByID(1), testEngineerDistrict1)); //7
            interventions.Add(Intervention.Factory.CreateIntervention(
                id++, types[0], application.Clients.GetClientByID(3), testEngineerDistrict3)); //8
            interventions.Add(Intervention.Factory.CreateIntervention(
                id++, types[2], application.Clients.GetClientByID(2), testEngineerDistrict2)); //9
            interventions.Add(Intervention.Factory.CreateIntervention(
                id++, types[1], application.Clients.GetClientByID(0), testEngineerDistrict0)); //10
            interventions.Add(Intervention.Factory.CreateIntervention(
                id++, types[0], application.Clients.GetClientByID(5), testEngineerDistrict5)); //11

            //Approve a few interventions
            interventions[4].Approve(testEngineerDistrict0);
            interventions[5].Approve(testEngineerDistrict1);
            interventions[7].Approve(testEngineerDistrict1);
            interventions[10].Approve(testEngineerDistrict0);
        }

        public void Add(Intervention intervention)
        {
            interventions.Add(intervention);
        }

        public int Count
        {
            get { return interventions.Count; }
        }

        public Intervention this[int i]
        {
            get { return interventions[i]; }
        }
    }
}
