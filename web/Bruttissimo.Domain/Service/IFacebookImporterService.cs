namespace Bruttissimo.Domain
{
    public interface IFacebookImporterService
    {
        void Import(FacebookImportOptions opts);
    }
}
