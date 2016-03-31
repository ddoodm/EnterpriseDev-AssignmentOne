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
        [TestMethod]
        public void Intervention_Create_All_Data_Supplied_Success()
        {
            InterventionType interventionType =
                InterventionTypes.SupplyInstallPortableToilet;

            Client client = new Client
                (
                "Foobar Family",
                "1 Madeup Lane, Fakeland",
                Districts.UrbanIndonesia
                );

            SiteEngineer siteEngineer = new SiteEngineer();

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
    }
}
