using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EvaluationWebApplication.Models.CFT;
using EvaluationWebApplication.Models.ViewModels;

namespace EvaluationWebApplication.Controllers
{
    [Authorize]
    public class TimeController : Controller
    {
        private CFTDbContext context = new CFTDbContext();
        private TimeEntryViewModel timeModel = new TimeEntryViewModel();
        //private Employee employee;

        public IActionResult Index(string email)
        {
            timeModel.Employee = context.Employees.FirstOrDefault(empl => empl.Email == email);
            //List<TimeEntry> TimeEntries = new List<TimeEntry>();
            if (timeModel.Employee.TimeEntries != null && timeModel.TimeEntries.Count > 0)
                timeModel.TimeEntries = timeModel.Employee.TimeEntries.Where(timeEntry => timeEntry.Date.Month == DateTime.Today.Month && timeEntry.Date.Year == DateTime.Today.Year).ToList();
            return View(timeModel);
        }
    }
}