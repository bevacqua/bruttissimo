using Bruttissimo.Domain.Entity.Entities;

namespace Bruttissimo.Domain.Service
{
    public interface ITwitterExporterService
    {
        void Export(TwitterExportLog entry);
    }
}