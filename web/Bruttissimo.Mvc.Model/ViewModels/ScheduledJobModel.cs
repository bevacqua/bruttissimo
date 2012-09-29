using System;

namespace Bruttissimo.Mvc.Model.ViewModels
{
    public class ScheduledJobModel
    {
        public string Guid { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
    }
}
