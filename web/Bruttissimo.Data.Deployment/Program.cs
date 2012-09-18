using Bruttissimo.Common;

namespace Bruttissimo.Data.Deployment
{
    public static class Program
    {
        public static int Main()
        {
            var runtimeEnvironment = Config.test;
            return runtimeEnvironment;

            UpgradeTool deployTool = new UpgradeTool();
            int exitCode = deployTool.Execute();
            return exitCode;
        }
    }
}
