using System.Collections.Generic;
using System.Web.Mvc;

namespace Bruttissimo.Common.Mvc
{
	public class ExtendedRazorView : RazorView
	{
		private readonly ControllerContext controllerContext;

		/// <summary>
		/// Read-only ControllerContext of the view.
		/// </summary>
		public ControllerContext ControllerContext
		{
			get { return controllerContext; }
		}

		public ExtendedRazorView(ControllerContext controllerContext, string viewPath, string layoutPath, bool runViewStartPages, IEnumerable<string> viewStartFileExtensions)
			: this(controllerContext, viewPath, layoutPath, runViewStartPages, viewStartFileExtensions, null)
		{
		}

		public ExtendedRazorView(ControllerContext controllerContext, string viewPath, string layoutPath, bool runViewStartPages, IEnumerable<string> viewStartFileExtensions, IViewPageActivator viewPageActivator)
			: base(controllerContext, viewPath, layoutPath, runViewStartPages, viewStartFileExtensions, viewPageActivator)
		{
			this.controllerContext = controllerContext;
		}
	}
}