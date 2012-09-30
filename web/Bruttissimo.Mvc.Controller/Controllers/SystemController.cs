using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Mvc.Core.Attributes;
using Bruttissimo.Common.Mvc.Core.Controllers;
using Bruttissimo.Common.Static;
using Bruttissimo.Domain.Entity.Constants;
using Bruttissimo.Domain.Entity.Entities;
using Bruttissimo.Domain.Service;
using Bruttissimo.Mvc.Model.ViewModels;

namespace Bruttissimo.Mvc.Controller.Controllers
{
    public class SystemController : ExtendedController
    {
        private readonly ILogService logService;

        public SystemController(ILogService logService)
        {
            Ensure.That(() => logService).IsNotNull();

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
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [NotAjax]
        [ExtendedAuthorize(Roles = Rights.CanAccessApplicationVariables)]
        public ActionResult Environment()
        {
            IList<KeyValuePair<string, string>> model = Config.AsKeyValuePairs();
            return View(model);
        }

        [HttpGet]
        [NotAjax]
        [ExtendedAuthorize(Roles = Rights.CanResetApplicationPool)]
        public ActionResult Reset()
        {
            HttpRuntime.UnloadAppDomain();
            return RedirectToAction("Index", "Home");
        }
    }
}
