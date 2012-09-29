using System;
using Bruttissimo.Common;
using Bruttissimo.Common.InversionOfControl;
using Bruttissimo.Common.Mvc;

namespace Bruttissimo.Domain.Logic
{
	public abstract class BaseService
	{
		private readonly Lazy<IUrlHelper> urlHelperLazy;
		private readonly Lazy<IMapper> mapperLazy;

	    protected IUrlHelper urlHelper
	    {
		    get { return urlHelperLazy.Value; }
	    }

		protected IMapper mapper
		{
			get { return mapperLazy.Value; }
		}

        protected BaseService()
		{
			urlHelperLazy = IoC.Container.Resolve<Lazy<IUrlHelper>>();
			mapperLazy = IoC.Container.Resolve<Lazy<IMapper>>();
		}
	}
}