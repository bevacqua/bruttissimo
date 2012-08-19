using System;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

namespace Bruttissimo.Domain.Logic
{
    public class PictureStorageRepository : IPictureStorageRepository
    {
        private readonly HttpContextBase httpContext;

        public PictureStorageRepository(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }
            this.httpContext = httpContext;
        }

        internal string GetPhysicalPath(string id, out string filename, out string folder, out string relativePath)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }
            filename = string.Concat(id, ".jpg");
            folder = Common.Resources.Constants.ImageUploadFolder;
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

        public void Save(System.Drawing.Image image, string id)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }
            string physicalPath = GetPhysicalPath(id);
            if (!File.Exists(physicalPath))
            {
                image.Save(physicalPath, ImageFormat.Jpeg);
            }
        }

        public System.Drawing.Image Load(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }
            string physicalPath = GetPhysicalPath(id);
            if (File.Exists(physicalPath))
            {
                return System.Drawing.Image.FromFile(physicalPath);
            }
            return null;
        }
    }
}
