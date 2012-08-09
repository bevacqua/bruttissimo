using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Bruttissimo.Common;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Data.Dapper;
using Bruttissimo.Domain.Logic;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using StackExchange.Profiling;
using StackExchange.Profiling.Data;

namespace Bruttissimo.Mvc.Windsor
{
	/// <summary>
	/// Registers all repositories.
	/// </summary>
	public class RepositoryInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			// Domain Logic assembly repositories.
			container.Register(
				AllTypes.FromAssemblyContaining<EmailRepository>()
					.Where(t => t.Name.EndsWith("Repository"))
					.WithService.Select(IoC.SelectByInterfaceConvention)
					.LifestylePerWebRequest()
			);

			// Dapper assembly repositories.
			container.Register(
				AllTypes.FromAssemblyContaining<UserRepository>()
					.Where(t => t.Name.EndsWith("Repository"))
					.WithService.Select(IoC.SelectByInterfaceConvention)
					.LifestylePerWebRequest()
			);

			// IDbConnection component.
			container.Register(
				Component
					.For<IDbConnection>()
					.UsingFactoryMethod(InstanceDbConnection)
					.OnCreate(c => c.Open())
					.OnDestroy(DestroyDbConnection)
					.LifestylePerWebRequest()
			);
		}

		private IDbConnection InstanceDbConnection()
		{
			string connectionString = ConnectionString("SqlConnection");
			DbConnection connection = new SqlConnection(connectionString);
			RichErrorDbConnection profiled = new RichErrorDbConnection(connection, MiniProfiler.Current); // wraps MiniProfiler's ProfiledDbConnection.
			return profiled;
		}

		private void DestroyDbConnection(IDbConnection connection)
		{
			ProfiledDbConnection profiled = connection as ProfiledDbConnection;
			if (profiled != null) // for some reason, MiniProfiler profiled Db Connections are disposed at some point.
			{
				if (profiled.WrappedConnection != null)
				{
					profiled.WrappedConnection.Close();
				}
			}
			else
			{
				connection.Close();
			}
		}

		private string ConnectionString(string key)
		{
			string connectionString = Config.GetConnectionString(key);
			return connectionString;
		}
	}
}