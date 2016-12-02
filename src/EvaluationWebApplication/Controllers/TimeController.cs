using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EvaluationWebApplication.Models.CFT;

namespace EvaluationWebApplication.Controllers
{
    [Authorize]
    public class TimeController : Controller
    {
        private CFTDbContext context = new CFTDbContext();
        private Employee employee;

        public IActionResult Index(string email)
        {
            employee = context.Employees.FirstOrDefault(empl => empl.Email == email);
            List<TimeEntry> TimeEntries = new List<TimeEntry>();
            if (employee.TimeEntries != null)
                TimeEntries = employee.TimeEntries.Where(timeEntry => timeEntry.Date.Month == DateTime.Today.Month && timeEntry.Date.Year == DateTime.Today.Year).ToList();
            return View(TimeEntries);
        }
    }
}