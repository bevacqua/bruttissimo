﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
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

        public JobsController(IJobService jobService)
        {
            if (jobService == null)
            {
                throw new ArgumentNullException("jobService");
            }
            this.jobService = jobService;
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

            if (!ModelState.IsValid)
            {
                return InvalidModelState(model);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Schedule(Guid guid)
        {
            if (!jobService.ScheduleJob(guid)) // sanity.
            {
                ModelState.AddModelError("JobKey", Common.Resources.User.InvalidJobKey);
                return Schedule();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}
