using System.Collections.Generic;

namespace Bruttissimo.Domain.Social.Facebook.Entity
{
    internal class FacebookResponse<T> where T : class
    {
        public IList<T> Data { get; set; }
        public FacebookResponsePaging Paging { get; set; }
    }
}
