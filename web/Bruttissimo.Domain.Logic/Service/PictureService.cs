using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Bruttissimo.Common;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain.Logic
{
	public class PictureService : IPictureService
	{
		private readonly IPictureRepository pictureRepository;
		private readonly IPictureStorageRepository pictureStorageRepository;
		private readonly FileSystemHelper fsHelper;

		public PictureService(IPictureRepository pictureRepository, IPictureStorageRepository pictureStorageRepository, FileSystemHelper fsHelper)
		{
			if (pictureRepository == null)
			{
				throw new ArgumentNullException("pictureRepository");
			}
			if (pictureStorageRepository == null)
			{
				throw new ArgumentNullException("pictureStorageRepository");
			}
			if (fsHelper == null)
			{
				throw new ArgumentNullException("fsHelper");
			}
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
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
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
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
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
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}

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