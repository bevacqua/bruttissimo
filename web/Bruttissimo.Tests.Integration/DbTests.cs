using System.Data;
using System.Data.SqlClient;
using Bruttissimo.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bruttissimo.Tests.Integration
{
    [TestClass]
    public class DbTests
    {
        [TestInitialize]
        public void TestInit()
        {
        }

        [TestMethod]
        public void SqlConnection_CanBeEstablished()
        {
            string connectionString = Config.GetConnectionString("SQLSERVER_CONNECTION_STRING");
            IDbConnection connection = new SqlConnection(connectionString);
            connection.Open();
            connection.Close();
        }
    }
}
