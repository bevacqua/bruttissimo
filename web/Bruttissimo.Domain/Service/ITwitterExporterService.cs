using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain
{
    public interface ITwitterExporterService
    {
        void Export(TwitterExportLog entry);
    }
}