using System.Data;
using System.Data.SqlClient;
using Bruttissimo.Common.Static;
using Bruttissimo.Domain.Entity.Entities;
using Dapper;
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
            string connectionString = Config.GetConnectionString("SqlServerConnectionString");
            IDbConnection connection = new SqlConnection(connectionString);
            connection.Open();
            connection.Close();
        }

        [TestMethod]
        public void dapper_can_fetch_log_entries()
        {
            string connectionString = Config.GetConnectionString("SqlServerConnectionString");
            IDbConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sql = "select * from [log]";
            var logs = connection.Query<Log>(sql);
            connection.Close();
        }

        [TestMethod]
        public void dapper_can_fetch_smiley_entries()
        {
            string connectionString = Config.GetConnectionString("SqlServerConnectionString");
            IDbConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sql = "select * from [smiley]";
            var smileys = connection.Query<Smiley>(sql);
            connection.Close();
        }
    }
}
