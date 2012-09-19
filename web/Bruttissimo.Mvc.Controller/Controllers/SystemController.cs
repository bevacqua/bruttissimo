using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bruttissimo.Common;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Domain;
using Bruttissimo.Domain.Entity;
using Bruttissimo.Mvc.Model;

namespace Bruttissimo.Mvc.Controller
{
    public class SystemController : ExtendedController
    {
        private readonly ILogService logService;

        public SystemController(ILogService logService)
        {
            if (logService == null)
            {
                throw new ArgumentNullException("logService");
            }
            this.logService = logService;
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

        [HttpGet]
        [NotAjax]
        [ExtendedAuthorize(Roles = Rights.CanAccessApplicationVariables)]
        public ActionResult Environment()
        {
            IList<KeyValuePair<string, string>> model = Config.AsKeyValuePairs();
            return View(model);
        }

        private IEnumerable<ActionLinkModel> GetActions(IMiniPrincipal principal)
        {
            Permission[] permissions = new[]
            {
                permission(Rights.CanAccessApplicationLogs, Url.Action("Log"), "Log"),
                permission(Rights.CanAccessApplicationJobs, Url.Action("Environment", "System"), "Environment"),
                permission(Rights.CanAccessApplicationJobs, Url.Action("Index", "Jobs"), "Jobs")
            };
            return permissions.Where(p => principal.IsInRole(p.Role)).Select(p => p.Action);
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
