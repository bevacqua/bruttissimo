using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Domain.Entity;
using Bruttissimo.Domain.Logic;
using Bruttissimo.Mvc.Model;

namespace Bruttissimo.Mvc.Controller
{
    [ExtendedAuthorize(Roles = Rights.CanAccessApplicationJobs)]
    public class JobsController : ExtendedController
    {
        private readonly IJobService jobService;
        private readonly IMapper mapper;

        public JobsController(IJobService jobService, IMapper mapper)
        {
            if (jobService == null)
            {
                throw new ArgumentNullException("jobService");
            }
            if (mapper == null)
            {
                throw new ArgumentNullException("mapper");
            }
            this.jobService = jobService;
            this.mapper = mapper;
        }

        [HttpGet]
        [NotAjax]
        public ActionResult Index()
        {
            IList<JobDto> dto = jobService.GetScheduledJobs();
            IList<JobModel> model = mapper.Map<IList<JobDto>, IList<JobModel>>(dto);
            return View(model);
        }

        [HttpGet]
        [NotAjax]
        public ActionResult Schedule()
        {
            return View();
        }
    }
}
