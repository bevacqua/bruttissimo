namespace Bruttissimo.Domain
{
    public interface IFacebookImporterService
    {
        void Import(FacebookImportOptions options);
    }
}
