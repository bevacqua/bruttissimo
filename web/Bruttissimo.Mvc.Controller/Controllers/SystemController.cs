using System;
using System.Collections.Generic;
using System.Linq;
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
            Permission[] permissions = new[]
            {
                permission(Rights.CanAccessApplicationLogs, Url.Action("Log"), "Log"),
                permission(Rights.CanAccessApplicationJobs, Url.Action("Index", "Jobs"), "Jobs")
            };
            return permissions.Where(permission => principal.IsInRole(permission.Role)).Select(permission => permission.Action);
        }

        private class Permission
        {
            public string Role { get; set; }
            public ActionLinkModel Action { get; set; }
        }

        private Permission permission(string role, string url, string resourceKey)
        {
            return new Permission
            {
                Role = role,
                Action = new ActionLinkModel
                {
                    Url = url,
                    ResourceKey = resourceKey
                }
            };
        }
    }
}
