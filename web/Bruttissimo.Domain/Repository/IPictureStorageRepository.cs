using System.Drawing;

namespace Bruttissimo.Domain.Repository
{
    public interface IPictureStorageRepository
    {
        void Save(Image image, string id);
        Image Load(string id);
        string GetRelativePath(string id);
    }
}
