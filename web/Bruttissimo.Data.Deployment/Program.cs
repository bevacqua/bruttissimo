using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using DbUp;
using DbUp.Engine;
using DbUp.Engine.Output;
using log4net;

namespace Bruttissimo.Data.Deployment
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                throw new ArgumentOutOfRangeException("args", "A connection string argument is required");
            }
            string connectionString = args[0];

            Assembly assembly = typeof(Program).Assembly;

            ILog logger = LogManager.GetLogger(typeof(UpgradeEngine));
            IUpgradeLog log = new Log4NetAndConsoleUpgradeLog(logger);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            UpgradeEngine upgrader = DeployChanges.To
                .SqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(assembly)
                .LogTo(log)
                .Build();

            DatabaseUpgradeResult result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                logger.Error("An exception occurred while upgrading the database.", result.Error);

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
                return -1;
            }
            else
            {
                int count = result.Scripts.Count();

                stopwatch.Stop();
                TimeSpan elapsed = stopwatch.Elapsed;
                string duration = elapsed.ToString("mm:ss");

                Console.ForegroundColor = ConsoleColor.Green;
                log.WriteInformation("{0} scripts executed successfully. Done in {1}!", count, duration);
                Console.ResetColor();
                return 0;
            }
        }
    }
}
