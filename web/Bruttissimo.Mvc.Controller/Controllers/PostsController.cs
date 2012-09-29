using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Bruttissimo.Common;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Common.Resources;
using Bruttissimo.Domain;
using Bruttissimo.Domain.Entity;
using Bruttissimo.Mvc.Model;

namespace Bruttissimo.Mvc.Controller
{
    public class PostsController : ExtendedController
    {
        private readonly ILinkService linkService;
        private readonly IPostService postService;

        public PostsController(ILinkService linkService, IPostService postService)
        {
            Ensure.That(linkService, "linkService").IsNotNull();
            Ensure.That(postService, "postService").IsNotNull();

            this.linkService = linkService;
            this.postService = postService;
        }

        [HttpGet]
        [NotAjax]
        public ActionResult Index()
        {
            return List();
        }

        public ActionResult Until(long id) // "id" is the timestamp
        {
            return List(id);
        }

        [NonAction]
        internal ActionResult List(long? timestamp = null)
        {
            int count = Config.Defaults.PostListPageSize;
            IEnumerable<Post> posts = postService.GetLatest(timestamp, count);
            PostListModel model = mapper.Map<IEnumerable<Post>, PostListModel>(posts);
            return FlexibleView("List", model);
        }

        // TODO: hooks? or something better, there MUST be a better way to do this.
        protected override PartialViewResult PartialView(string viewName, object model)
        {
            IHasOpenGraphMetadata metadata = model as IHasOpenGraphMetadata;
            if (metadata != null)
            {
                HttpContext.Items[Constants.OpenGraphContextItem] = metadata.OpenGraph;
            }
            return base.PartialView(viewName, model);
        }

        protected override ViewResult View(IView view, object model)
        {
            return base.View(view, model);
        }

        protected override ViewResult View(string viewName, string masterName, object model)
        {
            return base.View(viewName, masterName, model);
        }

        [HttpGet]
        [NotAjax]
        [ExtendedAuthorize]
        public ViewResult Create()
        {
            PostCreationModel model = new PostCreationModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken(Salt = "c!#3s")]
        [ExtendedAuthorize]
        public ActionResult Create(PostCreationModel model, IMiniPrincipal principal)
        {
            if (!ModelState.IsValid)
            {
                return InvalidModelState(model);
            }
            LinkResult parsed = linkService.ParseUserInput(model.Link);
            if (parsed.Result == LinkParseResult.Valid)
            {
                Post post = postService.Create(parsed.Link, model.UserMessage, principal.User);
                string details = DetailsRoute(post);
                return Redirect(details); // redirect to the newly created post.
            }
            return View(model);
        }
        
        [HttpGet]
        [NotAjax]
        [ExtendedAuthorize]
        public ActionResult Preview()
        {
            return RedirectToActionPermanent("Create", "Posts"); // lets be graceful.
        }

        [HttpPost]
        [AjaxOnly]
        [ExtendedAuthorize]
        public JsonResult Preview(string input)
        {
            LinkResult parsed = linkService.ParseUserInput(input);
            switch (parsed.Result)
            {
                default:
                case LinkParseResult.Invalid:
                {
                    return Json(new { faulted = "invalid" });
                }
                case LinkParseResult.Used:
                {
                    long? postId = parsed.Link.PostId;
                    if (postId.HasValue)
                    {
                        Post post = postService.GetById(postId.Value);
                        return Json(new
                        {
                            faulted = "used",
                            link = DetailsRoute(post),
                            id = postId
                        });
                    }
                    else
                    {
                        return Json(new { faulted = "invalid" });
                    }
                }
                case LinkParseResult.Valid:
                {
                    Link link = parsed.Link;
                    if (link.Description != null && link.Description.Length > 200)
                    {
                        link.Description = link.Description.Substring(0, 200);
                    }
                    LinkModel model = mapper.Map<Link, LinkModel>(link);
                    return AjaxView(model);
                }
            }
        }

        [NonAction]
        internal string DetailsRoute(Post post)
        {
            return urlHelper.Action("Details", "Posts", new
            {
                id = post.Id,
                slug = postService.GetTitleSlug(post)
            });
        }

        [HttpGet]
        [NotAjax]
        public ActionResult Details(long id, string slug)
        {
            Post post = postService.GetById(id, true, true);
            if (post == null)
            {
                return NotFound();
            }
            string titleSlug = postService.GetTitleSlug(post);
            if (slug != titleSlug) // favor consistency and single-endpoint posts.
            {
                return RedirectToActionPermanent("Details", "Posts", new { id, slug = titleSlug });
            }
            PostModel model = mapper.Map<Post, PostModel>(post);
            return View(model);
        }
    }
}
