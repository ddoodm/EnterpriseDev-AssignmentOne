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
        #region Shared Test Data
        private Client testClient;
        private SiteEngineer testEngineer;

        private const uint NUM_TEST_DISTRICTS = 3;
        private District testDistrictA, testDistrictB, testDistrictC;

        private InterventionTypes interventionTypes;
        #endregion

        #region Helper Data Creation Functions
        private InterventionType CreateTestInterventionType()
        {
            Assert.IsTrue(interventionTypes.Count >= 1, "There are no Intervention Types");

            return interventionTypes[0];
        }

        private Client CreateTestClient()
        {
            return new Client
                ( 10, "Foobar Family", "1 Madeup Lane, Fakeland", testDistrictA );
        }

        private void CreateTestDistricts()
        {
            Districts.PopulateDistricts();
            Assert.IsTrue(Districts.Count >= NUM_TEST_DISTRICTS,
                "There are not enough districts for testing.");

            testDistrictA = Districts.GetDistrictByID(0);
            testDistrictB = Districts.GetDistrictByID(1);
            testDistrictC = Districts.GetDistrictByID(2);
        }

        private SiteEngineer CreateTestSiteEngineer()
        {
            return new SiteEngineer
                ("Robert Markson", "markson.robert", "plaintextPassword",
                testDistrictA, 6, 500);
        }

        private Intervention CreateTestIntervention()
        {
            InterventionType interventionType = CreateTestInterventionType();

            return Intervention.Factory.CreateIntervention
                (interventionType, testClient, testEngineer);
        }
        #endregion

        [TestInitialize]
        public void Setup()
        {
            interventionTypes = new InterventionTypes();
            //districts = new DistrictsTests();

            CreateTestDistricts();
            testClient = CreateTestClient();
            testEngineer = CreateTestSiteEngineer();
        }

        #region Creation Tests
        /// <summary>
        /// Create an Intervention, given all parameters
        /// </summary>
        [TestMethod]
        public void Intervention_Create_All_Data_Supplied_Success()
        {
            InterventionType interventionType = CreateTestInterventionType();

            Intervention intervention = Intervention.Factory.CreateIntervention
                (interventionType, testClient, testEngineer,
                2, 400, DateTime.Now.AddDays(10));

            Assert.IsNotNull(intervention);
        }

        /// <summary>
        /// Create an Intervention, given only its type, siteEngineer and client
        /// </summary>
        [TestMethod]
        public void Intervention_Create_No_Data_Supplied_Success()
        {
            Intervention intervention = CreateTestIntervention();

            Assert.IsNotNull(intervention);
        }

        /// <summary>
        /// Instantiates an Intervention where the Client and Site Engineer
        /// Districts do not match. This operation should not be permitted
        /// by the User Interface, and should throw an Argument Exception
        /// if encountered. 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Intervention_Create_District_Discrepancy_Failure()
        {
            InterventionType interventionType = CreateTestInterventionType();

            // Create a new Engineer who does not service 'testDistrictA'
            SiteEngineer remoteEngineer = new SiteEngineer
                ("Markus Markson", "markson.markus", "aBcDe_12$45",
                testDistrictB, interventionType.Labour + 1, interventionType.Cost + 100);

            // Expected argument exception:
            Intervention intervention = Intervention.Factory.CreateIntervention
                (interventionType, testClient, remoteEngineer);

            Assert.Fail("Instantiation of Intervention with mismatched districts should result in an ArgumentException");
        }
        #endregion

        #region Approval Tests
        /// <summary>
        /// Creates an Intervention and attempts to approve it
        /// using the Engineer who proposed the Intervention.
        /// </summary>
        [TestMethod]
        public void Intervention_Approve_By_Engineer_Success()
        {
            InterventionType interventionType = CreateTestInterventionType();

            // Create a new Engineer that can approve the new intervention
            SiteEngineer testEngineer = new SiteEngineer
                ("Markus Markson", "markson.markus", "aBcDe_12$45",
                testDistrictA, interventionType.Labour + 1, interventionType.Cost + 100);

            Intervention intervention = Intervention.Factory.CreateIntervention
                ( interventionType, testClient, testEngineer );

            // Attempt to approve the intervention by the Engineer who proposed it
            intervention.Approve(testEngineer);
        }

        /// <summary>
        /// Creates an Intervention and attempts to approve it by
        /// a Manager who operates in the same District as the Intervention.
        /// </summary>
        [TestMethod]
        public void Intervention_Approve_By_Manager_Success()
        {
            InterventionType interventionType = CreateTestInterventionType();

            // Create a new Engineer
            SiteEngineer testEngineer = new SiteEngineer
                ("Markus Markson", "markson.markus", "aBcDe_12$45",
                testDistrictA, interventionType.Labour + 1, interventionType.Cost + 100);

            Intervention intervention = Intervention.Factory.CreateIntervention
                (interventionType, testClient, testEngineer);

            // Create a Manager who operates in the same District as the Intervention
            Manager testManager = new Manager
                ("Bob Bobson", "bobson.bob", "dCmEp_98T65",
                intervention.District, interventionType.Labour + 1, interventionType.Cost + 100);

            // Attempt to approve the intervention by a Manager of the same district
            intervention.Approve(testManager);
        }

        /// <summary>
        /// Attempts to approve an Intervention by an Engineer other than
        /// the Engineer who made the proposition. 
        /// 
        /// Expects an Argument Exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Intervention_Approve_By_Distinct_Engineer_Failure()
        {
            Intervention intervention = CreateTestIntervention();

            // Create a new Engineer who would otherwise be permitted to approve the Intervention
            SiteEngineer testEngineer = new SiteEngineer
                ("Markus Markson", "markson.markus", "aBcDe_12$45",
                intervention.District, intervention.Labour + 1, intervention.Cost + 100);

            // Attempt to approve the intervention by an Engineer who did not propose it
            // Should throw an Argument Exception
            intervention.Approve(testEngineer);

            Assert.Fail("A Site Engineer was permitted to approve an intervention that they did not create.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Intervention_Approve_By_Foreign_Manager_Failure()
        {
            InterventionType interventionType = CreateTestInterventionType();

            // Create a new Engineer
            SiteEngineer testEngineer = new SiteEngineer
                ("Markus Markson", "markson.markus", "aBcDe_12$45",
                testDistrictA, interventionType.Labour + 1, interventionType.Cost + 100);

            Intervention intervention = Intervention.Factory.CreateIntervention
                (interventionType, testClient, testEngineer);

            // Create a Manager who does not operate in the same district as the Intervention
            Manager testManager = new Manager
                ("Bob Bobson", "bobson.bob", "dCmEp_98T65",
                testDistrictB, interventionType.Labour + 1, interventionType.Cost + 100);

            // Attempt to approve the intervention by a Manager who does not operate in the same District
            intervention.Approve(testManager);

            Assert.Fail("A Manager was permitted to approve an Intervention that is proposed for a District that the Manager does not operate in.");
        }
        #endregion

        #region Approval State Change Tests

        #endregion
    }
}
