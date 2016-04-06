using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ENETCare.IMS;
using ENETCare.IMS.Interventions;
using ENETCare.IMS.Users;

namespace ENETCare.IMS.Tests
{
    [TestClass]
    public class UsersTest
    {
        [TestInitialize]
        public void Setup()
        {
            Districts.PopulateDistricts();
        }

        [TestMethod]
        public void Check_Manager_District()
        {
            Manager testManager = new Manager("Bob Bobson", "bobson.bob", "dCmEp_98T65",
                Districts.GetDistrictByID(1), 50, 50);

            Assert.IsTrue(testManager.District.Name == "Rural Indonesia");
        }
    }
}
