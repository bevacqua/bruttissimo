using System;
using System.Web.Mvc;

namespace Bruttissimo.Common.Mvc
{
    public class ExtendedControllerContext : ControllerContext
    {
        public Guid Guid { get; private set; }

        public ExtendedControllerContext(ControllerContext context)
            : this(context, Guid.NewGuid())
        {
        }

        public ExtendedControllerContext(ControllerContext context, Guid guid)
            : base(context)
        {
            Guid = guid;
        }
    }
}
