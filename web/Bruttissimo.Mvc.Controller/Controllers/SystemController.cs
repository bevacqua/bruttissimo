using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Domain;
using Bruttissimo.Domain.Entity;
using Bruttissimo.Mvc.Model;

namespace Bruttissimo.Mvc.Controller
{
    public class SystemController : ExtendedController
    {
        private readonly ILogService logService;
        private readonly IMapper mapper;

        public SystemController(ILogService logService, IMapper mapper)
        {
            if (logService == null)
            {
                throw new ArgumentNullException("logService");
            }
            if (mapper == null)
            {
                throw new ArgumentNullException("mapper");
            }
            this.logService = logService;
            this.mapper = mapper;
        }

        [HttpGet]
        [NotAjax]
        [ExtendedAuthorize(Roles = Rights.CanAccessApplicationLogs)]
        public ActionResult Log()
        {
            IList<Log> logs = logService.GetLast(10);
            IList<LogModel> model = mapper.Map<IList<Log>, IList<LogModel>>(logs);
            return View(model);
        }
    }
}
