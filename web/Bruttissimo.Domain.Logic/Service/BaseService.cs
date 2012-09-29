using System;
using Bruttissimo.Common;
using Bruttissimo.Common.Interface;
using Bruttissimo.Common.InversionOfControl;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Common.Mvc.Interface;

namespace Bruttissimo.Domain.Logic.Service
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