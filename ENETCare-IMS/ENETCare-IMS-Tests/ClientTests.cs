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
        [TestInitialize]
        public void Setup()
        {
            Districts.PopulateDistricts();
            Clients.PopulateClients();
        }

        [TestMethod]
        public void Client_District_Is_Not_Null_If_Client_Initialised_With_District_ID()
        {
           Client client = new Client(0, "John Doe", "1234 Alphabet Street", 0);

           Assert.IsTrue(client.District != null);

        }
    }
}
