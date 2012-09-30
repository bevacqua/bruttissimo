using System;
using System.Web.Mvc;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Mvc.Core.Attributes;
using Bruttissimo.Common.Mvc.Core.Controllers;
using Bruttissimo.Domain.MiniMembership;
using Bruttissimo.Domain.Service;
using Bruttissimo.Mvc.Model.ViewModels;

namespace Bruttissimo.Mvc.Controller.Controllers
{
    public class CommentsController : ExtendedController
    {
        private readonly ICommentService commentService;

        public CommentsController(ICommentService commentService)
        {
            Ensure.That(() => commentService).IsNotNull();

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
