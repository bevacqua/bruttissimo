using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Domain;
using Bruttissimo.Domain.Logic;
using Bruttissimo.Mvc.Models;

namespace Bruttissimo.Mvc.Controllers
{
	public class PostsController : ExtendedController
	{
		private readonly ILinkService linkService;
		private readonly IPostService postService;
		private readonly UrlHelper urlHelper;

		public PostsController(ILinkService linkService, IPostService postService, UrlHelper urlHelper)
		{
			if (linkService == null)
			{
				throw new ArgumentNullException("linkService");
			}
			if (postService == null)
			{
				throw new ArgumentNullException("postService");
			}
			if (urlHelper == null)
			{
				throw new ArgumentNullException("urlHelper");
			}
			this.linkService = linkService;
			this.postService = postService;
			this.urlHelper = urlHelper;
		}

		#region Get

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
		public ActionResult List(long? timestamp = null, int count = 8)
		{
			PostListModel model = new PostListModel();
			model.OpenGraph = new OpenGraphModel(); // TODO: load
			model.Posts = postService.GetLatest(timestamp, count).Select(PostModelConverter).ToList();

			if (ControllerContext.IsChildAction)
			{
				return PartialView("Index", model);
			}
			else
			{
				return View("Index", model);
			}
		}

		private PostModel PostModelConverter(Post post)
		{
			Link link = post.Link;
			if (link == null)
			{
				throw new ArgumentException("post.Link can't be null");
			}
			if (link.Type == LinkType.Html)
			{
				return new PostedLinkModel
				{
				    Description = link.Description,
				    PictureUrl = link.Picture,
				    PostId = post.Id,
				    PostSlug = postService.GetTitleSlug(post),
				    Timestamp = post.Created,
				    Title = link.Title,
				    UserMessage = post.UserMessage,
				    UserDisplayName = post.User.DisplayName
				};
			}
			else if (link.Type == LinkType.Image)
			{
				return new PostedImageModel
				{
					PictureUrl = link.Picture,
					PostId = post.Id,
					PostSlug = postService.GetTitleSlug(post),
					Timestamp = post.Created,
					UserMessage = post.UserMessage,
					UserDisplayName = post.User.DisplayName
				};
			}
			return null;
		}

		#endregion

		#region Create

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
		public ActionResult Create(PostCreationModel model, MiniPrincipal principal)
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
			return View(model); // TODO: reload home?
		}

		[HttpPost]
		[AjaxOnly]
		[ExtendedAuthorize]
		public JsonResult Preview(string input)
		{
			LinkResult parsed = linkService.ParseUserInput(input);
			if (parsed.Result == LinkParseResult.Used)
			{
				long? postId = parsed.Link.PostId;
				if (postId.HasValue)
				{
					Post post = postService.GetById(postId.Value, false);
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
			else if (parsed.Result == LinkParseResult.Invalid)
			{
				return Json(new { faulted = "invalid" });
			}
			else
			{
				Link link = parsed.Link;
				if (link.Description != null && link.Description.Length > 200)
				{
					link.Description = link.Description.Substring(0, 200);
				}
				return AjaxView(link);
			}
		}

		#endregion

		[NonAction]
		public string DetailsRoute(Post post)
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
			Post post = postService.GetById(id);
			if (post == null)
			{
				return NotFound();
			}
			string titleSlug = postService.GetTitleSlug(post);
			if (slug != titleSlug) // favor consistency and single-endpoint posts.
			{
				return RedirectToActionPermanent("Details", "Posts", new { id, slug = titleSlug });
			}
			return View(); // TODO
		}
	}
}