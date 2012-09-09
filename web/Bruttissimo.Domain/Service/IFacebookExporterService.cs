using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain
{
    public interface IFacebookExporterService
    {
        void Export(FacebookExportLog entry);
    }
}