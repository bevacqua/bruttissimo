using System;
using System.Web.Mvc;

namespace Bruttissimo.Common.Mvc
{
	/// <summary>
	/// This attribute is tasked with transforming view and redirect results into AJAX responses during AJAX requests.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public class AjaxTransformActionResultAttribute : ActionFilterAttribute
	{
		private readonly string defaultTitle;

		public AjaxTransformActionResultAttribute(string defaultTitle)
		{
			if (defaultTitle.NullOrBlank())
			{
				throw new ArgumentNullException("defaultTitle");
			}
			this.defaultTitle = defaultTitle;
		}

		public override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			if (filterContext.Exception != null || !filterContext.HttpContext.Request.IsAjaxRequest())
			{
				return; // this filter only cares about AJAX requests that successfully produced an ActionResult.
			}
			var controller = filterContext.Controller as StringRenderingController;
			if (controller == null)
			{
				throw new InvalidOperationException(Resources.Error.AjaxTransformAttributeDecoration);
			}

			var viewResult = filterContext.Result as ViewResult;
			if (viewResult != null) // view result AJAX transformation
			{
				string title = controller.ViewBag.Title ?? defaultTitle;
				string container = controller.ViewBag.AjaxViewContainer;
				SeparationOfConcernsResult view = controller.PartialViewSeparationOfConcerns(viewResult.ViewName, null, viewResult.Model);
				string html = view.Html;
				string script = view.JavaScript;

				AjaxViewJsonResult ajaxView = new AjaxViewJsonResult(title, html, script, container);
				filterContext.Result = ajaxView;
			}

			var redirectResult = filterContext.Result as RedirectResult;
			if (redirectResult != null)
			{
				filterContext.Result = new JsonRedirectResult(redirectResult);
			}
		}
	}
}