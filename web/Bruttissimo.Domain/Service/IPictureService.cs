using System.Drawing;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain
{
    public interface IPictureService
    {
        Picture Persist(Image image, PictureSize size = PictureSize.All);
        Image ScaleImage(Image image, float scale);
        string SizeAndSaveImage(string name, Image image, int maxSizeInPixels);
    }
}
