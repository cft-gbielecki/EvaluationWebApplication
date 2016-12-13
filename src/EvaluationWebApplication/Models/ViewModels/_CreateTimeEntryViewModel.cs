using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationWebApplication.Models.ViewModels
{
    public class _CreateTimeEntryViewModel
    {
        public List<SelectListItem> EmployeeContracts { get; set; }
        public List<SelectListItem> EmployeeServices { get; set; }

        [Display(Name = "Billing Date")]
        public DateTime TimeEntryDate { get; set; }
        [Display(Name = "Contract")]
        public string TimeEntryContract { get; set; }
        [Display(Name = "Service")]
        public string TimeEntryService { get; set; }
        [Display(Name = "Time")]
        public string TimeEntryTime { get; set; }
        [Display(Name ="MakingUp")]
        public bool TimeEntryMakeUp { get; set; }
        [Display(Name = "Project")]
        public string TimeEntryProject { get; set; }
        [Display(Name = "Comment")]
        public string TimeEntryComment { get; set; }
    }
}
