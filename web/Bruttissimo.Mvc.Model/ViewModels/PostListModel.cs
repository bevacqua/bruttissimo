using System.Collections.Generic;

namespace Bruttissimo.Mvc.Model
{
    public class PostListModel : IHasOpenGraphMetadata
    {
        public OpenGraphModel OpenGraph { get; set; } // og:model just describes the latest post
        public IList<PostModel> Posts { get; set; }
        public bool HasMorePosts { get; set; }
    }
}