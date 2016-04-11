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
        public const string INTERVENTIONS_SESSION_INSTANCE_KEY = "Interventions";

        private static List<Intervention> interventions = new List<Intervention>();

        public Interventions()
        {
            interventions = new List<Intervention>();

            // Test engineer
            SiteEngineer testEngineer = new SiteEngineer
                ("Bill Williams", "williams.bill", "abc1234",
                Clients.GetClientByID(0).District, 12, 1000);

            // Populate interventions with fake data
            InterventionTypes types = new InterventionTypes();
            interventions.Add(Intervention.Factory.CreateIntervention(
                0, types[0], Clients.GetClientByID(0), testEngineer));
            interventions.Add(Intervention.Factory.CreateIntervention(
                1, types[1], Clients.GetClientByID(0), testEngineer));
            interventions.Add(Intervention.Factory.CreateIntervention(
                2, types[0], Clients.GetClientByID(0), testEngineer));
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
