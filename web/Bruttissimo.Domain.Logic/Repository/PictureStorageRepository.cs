using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Resources;
using Bruttissimo.Domain.Repository;

namespace Bruttissimo.Domain.Logic.Repository
{
    public class PictureStorageRepository : IPictureStorageRepository
    {
        private readonly HttpContextBase httpContext;

        public PictureStorageRepository(HttpContextBase httpContext)
        {
            Ensure.That(httpContext, "httpContext").IsNotNull();

            this.httpContext = httpContext;
        }

        internal string GetPhysicalPath(string id, out string filename, out string folder, out string relativePath)
        {
            Ensure.That(id, "id").IsNotNull();

            filename = string.Concat(id, ".jpg");
            folder = Constants.ImageUploadFolder;
            relativePath = Path.Combine(folder, filename);
            string physicalPath = httpContext.Server.MapPath(relativePath);
            return physicalPath;
        }

        internal string GetPhysicalPath(string id)
        {
            string filename;
            string folder;
            string relativePath;
            string physicalPath = GetPhysicalPath(id, out filename, out folder, out relativePath);
            return physicalPath;
        }

        public string GetRelativePath(string id)
        {
            string filename;
            string folder;
            string relativePath;
            GetPhysicalPath(id, out filename, out folder, out relativePath);
            return relativePath;
        }

        public void Save(Image image, string id)
        {
            Ensure.That(image, "image").IsNotNull();
            Ensure.That(id, "id").IsNotNull();

            string physicalPath = GetPhysicalPath(id);
            if (!File.Exists(physicalPath))
            {
                image.Save(physicalPath, ImageFormat.Jpeg);
            }
        }

        public Image Load(string id)
        {
            Ensure.That(id, "id").IsNotNull();

            string physicalPath = GetPhysicalPath(id);
            if (File.Exists(physicalPath))
            {
                return Image.FromFile(physicalPath);
            }
            return null;
        }
    }
}
