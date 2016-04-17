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
    }
}
