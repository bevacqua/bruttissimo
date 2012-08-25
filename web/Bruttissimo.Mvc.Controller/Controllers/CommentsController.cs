using System;
using System.Web.Mvc;
using Bruttissimo.Common;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Domain;

namespace Bruttissimo.Mvc.Controller
{
    public class CommentsController : ExtendedController
    {
        private readonly ICommentService commentService;
        private readonly IMapper mapper;

        public CommentsController(ICommentService commentService, IMapper mapper)
        {
            if (commentService == null)
            {
                throw new ArgumentNullException("commentService");
            }
            if (mapper == null)
            {
                throw new ArgumentNullException("mapper");
            }
            this.commentService = commentService;
            this.mapper = mapper;
        }

        [HttpGet]
        [NotAjax]
        [ExtendedAuthorize]
        public ActionResult New(long id)
        {
            return RedirectToActionPermanent("Details", "Posts", new { id }); // lets be graceful.
        }

        [HttpPost]
        [ExtendedAuthorize]
        public ActionResult New(long id, string comment, IMiniPrincipal principal)
        {
            throw new NotImplementedException();
        }
    }
}
