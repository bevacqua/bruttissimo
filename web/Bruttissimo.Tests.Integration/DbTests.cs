using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
        	string connectionString = ConfigurationManager.ConnectionStrings["SqlConnectionTest"].ConnectionString;
            IDbConnection connection = new SqlConnection(connectionString);
            connection.Open();
            connection.Close();
        }
	}
}
