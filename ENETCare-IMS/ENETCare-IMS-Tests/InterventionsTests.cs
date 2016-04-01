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
        private Client testClient;
        private SiteEngineer testEngineer;
        private District testDistrict;

        private InterventionTypes interventionTypes;
        private Districts districts;

        private InterventionType CreateTestInterventionType()
        {
            Assert.IsTrue(interventionTypes.Count >= 1, "There are no Intervention Types");

            return interventionTypes[0];
        }

        private Client CreateTestClient()
        {
            return new Client
                (
                "Foobar Family",
                "1 Madeup Lane, Fakeland",
                testDistrict
                );
        }

        private District CreateTestDistrict()
        {
            Assert.IsTrue(districts.Count >= 1, "There are no Districts");

            return districts[0];
        }

        private SiteEngineer CreateTestSiteEngineer()
        {
            return new SiteEngineer
                (
                "Robert Markson",
                "markson.robert",
                "plaintextPassword",
                testDistrict,
                6,
                500
                );
        }

        [TestInitialize]
        public void Setup()
        {
            interventionTypes = new InterventionTypes();
            districts = new Districts();

            testDistrict = CreateTestDistrict();
            testClient = CreateTestClient();
            testEngineer = CreateTestSiteEngineer();
        }

        /// <summary>
        /// Create an Intervention, given all parameters
        /// </summary>
        [TestMethod]
        public void Intervention_Create_All_Data_Supplied_Success()
        {
            InterventionType interventionType = CreateTestInterventionType();

            Intervention intervention = Intervention.Factory.CreateIntervention
                (
                interventionType,
                testClient,
                testEngineer,
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

            Intervention intervention = Intervention.Factory.CreateIntervention
                (
                interventionType,
                testClient,
                testEngineer
                );

            Assert.IsNotNull(intervention);
        }
    }
}
