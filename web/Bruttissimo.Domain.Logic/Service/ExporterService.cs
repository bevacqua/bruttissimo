using System.Collections.Generic;
using Bruttissimo.Domain.Entity.Entities;
using Bruttissimo.Domain.Entity.Entities.Interface;

namespace Bruttissimo.Domain.Logic.Service
{
    public abstract class ExporterService<TResponse> : BaseService where TResponse : class
    {
        public void Export(IExportLog log)
        {
            IList<Post> posts = GetPostsToExport();
            int exportCount = 0;

            foreach (Post post in posts)
            {
                var response = Send(post);
                if (response == null) // export failed.
                {
                    continue;
                }
                Update(post, response);
                exportCount++;
            }
            log.ExportCount = exportCount;
            log.PostCount = posts.Count;
        }

        protected abstract IList<Post> GetPostsToExport();
        protected abstract TResponse Send(Post post);
        protected abstract void Update(Post post, TResponse response);
    }
}