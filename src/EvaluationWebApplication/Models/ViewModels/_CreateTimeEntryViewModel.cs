using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationWebApplication.Models.ViewModels
{
    public class _CreateTimeEntryViewModel
    {
        public List<SelectListItem> EmployeeContracts { get; set; }
        public List<SelectListItem> EmployeeServices { get; set; }

        public DateTime TimeEntryDate { get; set; }
        public string TimeEntryContract { get; set; }
        public string TimeEntryService { get; set; }
        public string TimeEntryTime { get; set; }
        public bool TimeEntryMakeUp { get; set; }
        public string TimeEntryProject { get; set; }
        public string TimeEntryComment { get; set; }
    }
}
