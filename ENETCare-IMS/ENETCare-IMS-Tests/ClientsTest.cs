using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ENETCare.IMS.Tests
{
    [TestClass]
    public class ClientsTest
    {
        [TestInitialize]
        public void Setup()
        {
            Districts.PopulateDistricts();
            Clients.PopulateClients();
        }

        [TestMethod]
        public void Clients_Get_Client_By_ID_Method_Returns_Client()
        {
            int id = 0;
            Client client = Clients.GetClientByID(id);

            Assert.IsTrue(client.ID == id);

        }
    }
}
