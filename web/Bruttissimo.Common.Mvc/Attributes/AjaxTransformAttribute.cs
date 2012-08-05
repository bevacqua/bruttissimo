using System;
using System.Web.Mvc;

namespace Bruttissimo.Common.Mvc
{
	/// <summary>
	/// This attribute is tasked with transforming view and redirect results into AJAX responses during AJAX requests.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public class AjaxTransformAttribute : ActionFilterAttribute
	{
		private readonly string defaultTitle;

		public AjaxTransformAttribute(string defaultTitle)
		{
			if (defaultTitle.NullOrBlank())
			{
				throw new ArgumentNullException("defaultTitle");
			}
			this.defaultTitle = defaultTitle;
		}

		public override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			AjaxTransformFilter filter = new AjaxTransformFilter(defaultTitle);
			filter.OnActionExecuted(filterContext);
		}
	}
}