using System.IO;
using System.Web.Mvc;
using Bruttissimo.Common.Guard;

namespace Bruttissimo.Common.Mvc.Core.Engine
{
    public class ExtendedView : IView
    {
        private readonly IView view;
        private readonly ControllerContext controllerContext;

        /// <summary>
        /// Read-only ControllerContext of the view.
        /// </summary>
        public ControllerContext ControllerContext
        {
            get { return controllerContext; }
        }

        public ExtendedView(IView view, ControllerContext controllerContext)
        {
            Ensure.That(view, "view").IsNotNull();
            Ensure.That(controllerContext, "controllerContext").IsNotNull();

            this.view = view;
            this.controllerContext = controllerContext;
        }

        public void Render(ViewContext viewContext, TextWriter writer)
        {
            view.Render(viewContext, writer);
        }
    }
}
