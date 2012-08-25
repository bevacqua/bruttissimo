using System;
using System.Web.Mvc;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Domain;
using Bruttissimo.Domain.Entity;
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
            Comment comment = commentService.Create(model.Id, model.Message, principal.User);
            throw new NotImplementedException();
        }
    }
}
