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
        [ExtendedAuthorize(Roles = Rights.CanAccessSystemPanel)]
        public ActionResult Index(IMiniPrincipal principal)
        {
            IEnumerable<ActionLinkModel> model = GetActions(principal);
            return View(model);
        }

        private IEnumerable<ActionLinkModel> GetActions(IMiniPrincipal principal)
        {
            if (principal.IsInRole(Rights.CanAccessApplicationLogs))
            {
                yield return new ActionLinkModel
                {
                    Url = Url.Action("Log"),
                    ResourceKey = "Log"
                };
            }
            if (principal.IsInRole(Rights.CanAccessApplicationJobs))
            {
                yield return new ActionLinkModel
                {
                    Url = Url.Action("Jobs", "System"),
                    ResourceKey = "Jobs"
                };
            }
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
