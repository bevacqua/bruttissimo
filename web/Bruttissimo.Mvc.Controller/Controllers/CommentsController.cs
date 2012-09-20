using System;
using System.Web.Mvc;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Domain;
using Bruttissimo.Mvc.Model;

namespace Bruttissimo.Mvc.Controller
{
    public class CommentsController : ExtendedController
    {
        private readonly ICommentService commentService;

        public CommentsController(ICommentService commentService)
        {
            if (commentService == null)
            {
                throw new ArgumentNullException("commentService");
            }
            this.commentService = commentService;
        }

        [HttpPost]
        [ExtendedAuthorize]
        public ActionResult Create(CommentCreationModel model, IMiniPrincipal principal)
        {
            commentService.Create(model.Id, model.Message, principal.User, model.ParentId);
            return RedirectToAction("Details", "Posts", new { id = model.Id });
        }

        [ExtendedAuthorize]
        public ActionResult UpVote()
        {
            throw new NotImplementedException();
        }

        [ExtendedAuthorize]
        public ActionResult DownVote()
        {
            throw new NotImplementedException();
        }
    }
}
