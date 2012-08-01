using System;
using System.Web.Mvc;
using Castle.MicroKernel;

namespace Bruttissimo.Common.Mvc
{
	public sealed class ExtendedRazorViewEngine : RazorViewEngine
	{
		private readonly IKernel kernel;

		public ExtendedRazorViewEngine(IKernel kernel)
		{
			if (kernel == null)
			{
				throw new ArgumentNullException("kernel");
			}
			this.kernel = kernel;
		}

		protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
		{
			RegisterJavaScript(controllerContext, partialPath);
			return new ExtendedRazorView(controllerContext, partialPath, null, false, FileExtensions, ViewPageActivator);
		}

		protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
		{
			RegisterJavaScript(controllerContext, viewPath);
			return new ExtendedRazorView(controllerContext, viewPath, masterPath, true, FileExtensions, ViewPageActivator);
		}

		/// <summary>
		/// Manages script separation of concerns convention by loading the different javascript portions of each partial into a context item.
		/// </summary>
		private void RegisterJavaScript(ControllerContext controllerContext, string viewPath)
		{
			Guid? guid = null;
			ExtendedControllerContext extendedContext = GetExtendedControllerContext(controllerContext);
			if (extendedContext != null)
			{
				// When we render the view, we add JavaScript to the provided context, we identify contexts by using Guids.
				guid = extendedContext.Guid;
			}
			StringRenderingController controller = controllerContext.Controller as StringRenderingController;
			if (controller == null)
			{
				throw new InvalidOperationException(Resources.Error.ControllerBaseTypeMismatch);
			}
			if (viewPath.EndsWith(Resources.Constants.JavaScriptViewNamingExtension)) // sanity.
			{
				return; // prevent StackOverflowException.
			}
			string partial = controller.JavaScriptPartialViewString(viewPath, controller.ViewData.Model);
			if (partial != null)
			{
				JavaScriptHelper javaScriptHelper = kernel.Resolve<JavaScriptHelper>();
				javaScriptHelper.Register(viewPath, partial, guid);
			}
		}

		/// <summary>
		/// <para>Finds the ExtendedControllerContext associated with the View currently being rendered.</para>
		/// <para>If an ExtendedControllerContext is found, this means the JavaScript should be registered to that particular context.</para>
		/// </summary>
		private ExtendedControllerContext GetExtendedControllerContext(ControllerContext controllerContext)
		{
			var extendedContext = controllerContext as ExtendedControllerContext;
			if (extendedContext == null)
			{
				var viewContext = controllerContext as ViewContext;
				if (viewContext != null)
				{
					var razorView = viewContext.View as ExtendedRazorView;
					if (razorView != null)
					{
						extendedContext = razorView.ControllerContext as ExtendedControllerContext;
					}
				}
			}
			return extendedContext;
		}
	}
}