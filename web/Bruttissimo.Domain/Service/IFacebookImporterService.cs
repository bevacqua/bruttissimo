using Bruttissimo.Domain.DTO.Facebook;

namespace Bruttissimo.Domain.Service
{
    public interface IFacebookImporterService
    {
        void Import(FacebookImportOptions opts);
    }
}
