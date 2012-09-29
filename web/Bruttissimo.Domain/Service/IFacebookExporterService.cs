using Bruttissimo.Domain.Entity.Entities;

namespace Bruttissimo.Domain.Service
{
    public interface IFacebookExporterService
    {
        void Export(FacebookExportLog entry);
    }
}