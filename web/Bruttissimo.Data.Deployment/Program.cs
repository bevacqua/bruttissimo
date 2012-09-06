namespace Bruttissimo.Data.Deployment
{
    public static class Program
    {
        public static int Main()
        {
            DbUpgrader upgrader = new DbUpgrader();
            int exitCode = upgrader.Execute();
            return exitCode;
        }
    }
}
