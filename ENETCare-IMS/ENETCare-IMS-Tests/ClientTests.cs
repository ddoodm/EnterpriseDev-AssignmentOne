using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ENETCare.IMS;
using ENETCare.IMS.Interventions;
using ENETCare.IMS.Users;

namespace ENETCare.IMS.Tests
{
    [TestClass]
    public class ClientTests
    {
        private ENETCareDAO application;

        [TestInitialize]
        public void Setup()
        {
            application = new ENETCareDAO();
        }

        [TestMethod]
        public void Client_District_Is_Not_Null_If_Client_Initialised_With_District_ID()
        {
           Client client = new Client(
               0, "John Doe", "1234 Alphabet Street", application.Districts.GetDistrictByID(0));

           Assert.IsTrue(client.District != null);
        }
    }
}
