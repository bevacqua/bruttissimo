using System.Drawing;
using Bruttissimo.Domain.DTO.Picture;
using Bruttissimo.Domain.Entity.Entities;

namespace Bruttissimo.Domain.Service
{
    public interface IPictureService
    {
        Picture Persist(Image image, PictureSize size = PictureSize.All);
        Image ScaleImage(Image image, float scale);
        string SizeAndSaveImage(string name, Image image, int maxSizeInPixels);
    }
}
