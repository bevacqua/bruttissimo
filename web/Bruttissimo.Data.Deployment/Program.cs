namespace Bruttissimo.Data.Deployment
{
    public static class Program
    {
        public static int Main()
        {
            UpgradeTool deployTool = new UpgradeTool();
            int exitCode = deployTool.Execute();
            return exitCode;
        }
    }
}
