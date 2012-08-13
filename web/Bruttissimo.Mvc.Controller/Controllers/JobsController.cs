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
            IEnumerable<ScheduledJobDto> dto = jobService.GetScheduledJobs();
            IEnumerable<ScheduledJobModel> model = mapper.Map<IEnumerable<ScheduledJobDto>, IEnumerable<ScheduledJobModel>>(dto);
            return View(model);
        }

        [HttpGet]
        [NotAjax]
        public ActionResult Schedule()
        {
            IEnumerable<JobDto> dto = jobService.GetAvailableJobs();
            IEnumerable<JobModel> model = mapper.Map<IEnumerable<JobDto>, IEnumerable<JobModel>>(dto);
            return View(model);
        }

        [HttpPost]
        public ActionResult Schedule(Guid guid)
        {
            IEnumerable<JobDto> dto = jobService.GetAvailableJobs();
            if (dto.Any(job => job.Guid == guid.Stringify())) // sanity
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}