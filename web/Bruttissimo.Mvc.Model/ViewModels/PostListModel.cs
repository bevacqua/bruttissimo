using System.Collections.Generic;
using Bruttissimo.Mvc.Model.Interfaces;

namespace Bruttissimo.Mvc.Model.ViewModels
{
    public class PostListModel : IHasOpenGraphMetadata
    {
        public OpenGraphModel OpenGraph { get; set; } // og:model just describes the latest post
        public IList<PostModel> Posts { get; set; }
        public bool HasMorePosts { get; set; }
    }
}