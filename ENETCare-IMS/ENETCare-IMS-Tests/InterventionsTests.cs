using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ENETCare.IMS;
using ENETCare.IMS.Interventions;
using ENETCare.IMS.Users;

namespace ENETCare.IMS.Tests
{
    [TestClass]
    public class InterventionsTests
    {
        private InterventionType CreateTestInterventionType()
        {
            // Obtain the InterventionTypes database 
            InterventionTypes types = new InterventionTypes();
            Assert.IsTrue(types.Count >= 1, "There are no pre-defined Intervention Types");

            return types[0];
        }

        private Client CreateTestClient()
        {
            return new Client
                (
                "Foobar Family",
                "1 Madeup Lane, Fakeland",
                Districts.UrbanIndonesia
                );
        }

        private SiteEngineer CreateTestSiteEngineer()
        {
            return new SiteEngineer();
        }

        /// <summary>
        /// Create an Intervention, given all parameters
        /// </summary>
        [TestMethod]
        public void Intervention_Create_All_Data_Supplied_Success()
        {
            InterventionType interventionType = CreateTestInterventionType();

            Client client = CreateTestClient();

            SiteEngineer siteEngineer = CreateTestSiteEngineer();

            Intervention intervention = Intervention.Factory.CreateIntervention
                (
                interventionType,
                client,
                siteEngineer,
                2,
                400,
                DateTime.Now.AddDays(10)
                );

            Assert.IsNotNull(intervention);
        }

        /// <summary>
        /// Create an Intervention, given only its type, siteEngineer and client
        /// </summary>
        [TestMethod]
        public void Intervention_Create_No_Data_Supplied_Success()
        {
            InterventionType interventionType = CreateTestInterventionType();

            Client client = CreateTestClient();

            SiteEngineer siteEngineer = CreateTestSiteEngineer();

            Intervention intervention = Intervention.Factory.CreateIntervention
                (
                interventionType,
                client,
                siteEngineer
                );

            Assert.IsNotNull(intervention);
        }
    }
}
