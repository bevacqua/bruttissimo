using System;
using Bruttissimo.Common;

namespace Bruttissimo.Domain.Logic
{
	public abstract class BaseService
	{
		private readonly Lazy<IMapper> mapperLazy;
		
		protected IMapper mapper
		{
			get { return mapperLazy.Value; }
		}

		protected BaseService()
		{
			mapperLazy = IoC.Container.Resolve<Lazy<IMapper>>();
		}
	}
}