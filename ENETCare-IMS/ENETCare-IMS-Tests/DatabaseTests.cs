using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ENETCare.IMS.Tests
{
    [TestClass]
    public class DatabaseTests
    {
        /// <summary>
        /// Sets the path of the Data directory for
        /// interfacing with the database.
        /// </summary>
        /// <param name="context"></param>
        [AssemblyInitialize]
        public static void SetupDataDirectory(TestContext context)
        {
            string path = Path.GetFullPath(Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                @"..\..\..\ENETCare-IMS-Data"));
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
        }

        [TestMethod]
        public void Connection_OpenClose_Success()
        {
            string connectionString = ConfigurationManager
                .ConnectionStrings["DatabaseConnection"]
                .ConnectionString;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            conn.Close();
        }
    }
}
