using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Bruttissimo.Common;
using Bruttissimo.Common.Extensions;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Helpers;
using Bruttissimo.Common.Utility;
using Bruttissimo.Domain.DTO.Picture;
using Bruttissimo.Domain.Entity.Entities;
using Bruttissimo.Domain.Repository;
using Bruttissimo.Domain.Service;

namespace Bruttissimo.Domain.Logic.Service
{
    public class PictureService : IPictureService
    {
        private readonly IPictureRepository pictureRepository;
        private readonly IPictureStorageRepository pictureStorageRepository;
        private readonly FileSystemHelper fsHelper;

        public PictureService(IPictureRepository pictureRepository, IPictureStorageRepository pictureStorageRepository, FileSystemHelper fsHelper)
        {
            Ensure.That(pictureRepository, "pictureRepository").IsNotNull();
            Ensure.That(pictureStorageRepository, "pictureStorageRepository").IsNotNull();
            Ensure.That(fsHelper, "fsHelper").IsNotNull();

            this.pictureRepository = pictureRepository;
            this.pictureStorageRepository = pictureStorageRepository;
            this.fsHelper = fsHelper;
        }

        /// <summary>
        /// Saves the provided image with the specified dimensions and then adds references to the results in the persistance store.
        /// Then the image object is disposed, and an image entity is returned.
        /// </summary>
        public Picture Persist(Image image, PictureSize size = PictureSize.All)
        {
            throw new NotImplementedException();

            Ensure.That(image, "image").IsNotNull();

            if (size == PictureSize.None)
            {
                return null;
            }

            Picture picture = null; // pictureRepository.Create();
            string filename = fsHelper.GenerateRandomFilenameFormat();

            using (image)
            {
                foreach (KeyValuePair<PictureSize, Action<Picture, string>> option in GetResizeOptions())
                {
                    if (size.HasFlag(option.Key))
                    {
                        int max = option.Key.GetAttribute<PicturePixelsAttribute>().Pixels;
                        option.Value(picture, SizeAndSaveImage(filename, image, max));
                    }
                }
                return picture;
            }
        }

        /// <summary>
        /// Avoids code repetition by using delegates to assign images to the entity according to the different image sizes.
        /// </summary>
        internal IEnumerable<KeyValuePair<PictureSize, Action<Picture, string>>> GetResizeOptions()
        {
            yield return new KeyValuePair<PictureSize, Action<Picture, string>>(PictureSize.Thumbnail, (i, r) => i.Thumbnail = r);
            yield return new KeyValuePair<PictureSize, Action<Picture, string>>(PictureSize.Regular, (i, r) => i.Regular = r);
            yield return new KeyValuePair<PictureSize, Action<Picture, string>>(PictureSize.Large, (i, r) => i.Large = r);
        }

        /// <summary>
        /// Determines whether an image should be resized to fit a particular format, saves it and returns the physical path to the saved file.
        /// </summary>
        public string SizeAndSaveImage(string name, Image image, int maxSizeInPixels)
        {
            Ensure.That(name, "name").IsNotNull();
            Ensure.That(image, "image").IsNotNull();

            if (image.Width <= maxSizeInPixels && image.Height <= maxSizeInPixels) // no need to resize
            {
                string id = name.FormatWith("src");
                pictureStorageRepository.Save(image, id);
                return id;
            }

            // get percentage.
            int size = Math.Max(image.Width, image.Height);
            float scale = (float)maxSizeInPixels / size;

            using (Image scaled = ScaleImage(image, scale))
            {
                string id = name.FormatWith(maxSizeInPixels);
                pictureStorageRepository.Save(scaled, id);
                return id;
            }
        }

        public Image ScaleImage(Image image, float scale)
        {
            Ensure.That(image, "image").IsNotNull();

            int sourceWidth = image.Width;
            int sourceHeight = image.Height;
            Rectangle source = new Rectangle(0, 0, sourceWidth, sourceHeight);

            int destWidth = (int)(sourceWidth * scale);
            int destHeight = (int)(sourceHeight * scale);
            Rectangle dest = new Rectangle(0, 0, destWidth, destHeight);

            Bitmap result = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);
            result.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (Graphics graphics = Graphics.FromImage(result))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(image, dest, source, GraphicsUnit.Pixel);
            }

            return result;
        }
    }
}
