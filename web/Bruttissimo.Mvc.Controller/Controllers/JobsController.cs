﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Mvc.Core.Attributes;
using Bruttissimo.Common.Mvc.Core.Controllers;
using Bruttissimo.Domain.Entity.Constants;
using Bruttissimo.Domain.Entity.DTO;
using Bruttissimo.Domain.Service;
using Bruttissimo.Mvc.Model.ViewModels;

namespace Bruttissimo.Mvc.Controller.Controllers
{
    [ExtendedAuthorize(Roles = Rights.CanAccessApplicationJobs)]
    public class JobsController : ExtendedController
    {
        private readonly IJobService jobService;

        public JobsController(IJobService jobService)
        {
            Ensure.That(() => jobService).IsNotNull();

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
