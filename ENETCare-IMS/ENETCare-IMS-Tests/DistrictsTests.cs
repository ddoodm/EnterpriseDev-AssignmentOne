﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ENETCare.IMS.Tests
{
    [TestClass]
    public class DistrictsTests
    {
        [TestMethod]
        public void Get_District_By_ID_Returns_Valid_District()
        {
            int id = 0;
            District district = Districts.GetDistrictByID(id);

            Assert.IsTrue(district.ID == id);
        }
    }
}