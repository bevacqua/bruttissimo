namespace Bruttissimo.Domain
{
    public interface IPictureStorageRepository
    {
        void Save(System.Drawing.Image image, string id);
        System.Drawing.Image Load(string id);
        string GetRelativePath(string id);
    }
}
