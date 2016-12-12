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
        private TimeEntryViewModel timeModel;
        //private Employee employee;

        public IActionResult Index(string email)
        {
            timeModel = new TimeEntryViewModel(context.Employees.FirstOrDefault(empl => empl.Email == email));
            //timeModel.Employee = context.Employees.FirstOrDefault(empl => empl.Email == email);
            //List<TimeEntry> TimeEntries = new List<TimeEntry>();
            if (timeModel.Employee.TimeEntries != null && timeModel.TimeEntries.Count > 0)
                timeModel.TimeEntries = timeModel.Employee.TimeEntries.Where(timeEntry => timeEntry.Date.Month == DateTime.Today.Month && timeEntry.Date.Year == DateTime.Today.Year).ToList();
            return View(timeModel);
        }

        [HttpPost]
        public IActionResult Create(_CreateTimeEntryViewModel timeModelCreate)
        {
            TimeEntry timeEntry = new TimeEntry()
            {
                Date = timeModelCreate.TimeEntryDate,
                EmployeeName = timeModel.Employee.FirstName,
                EmployeeSurname = timeModel.Employee.SecondName,
                Client = timeModel.Clients.FirstOrDefault(client => client.ClientName == timeModelCreate.TimeEntryContract.Substring(0, timeModelCreate.TimeEntryContract.IndexOf(':') - 1)).ClientName,
                Comment = timeModelCreate.TimeEntryComment,
                Contract = timeModelCreate.TimeEntryContract,
                Employee = timeModel.Employee,
                EmployeeID = timeModel.Employee.EmployeeID,
                MakeUp = timeModelCreate.TimeEntryMakeUp,
                Project = timeModelCreate.TimeEntryProject,
                Service = timeModelCreate.TimeEntryService,
                Time = timeModelCreate.TimeEntryTime
            };
            context.TimeEntries.Add(timeEntry);
            context.SaveChanges();

            ServiceClass serviceEntry = new ServiceClass(
                (EvaluationWebApplication.Models.CFT.Services)Enum.Parse(typeof(EvaluationWebApplication.Models.CFT.Services),
                            timeModelCreate.TimeEntryService.Substring(timeModelCreate.TimeEntryService.IndexOf(") ") + 2)));
            serviceEntry.EntryID = timeEntry.EntryID;
            context.Service.Add(serviceEntry);
            context.SaveChanges();

            return RedirectToAction("Index", timeModel.Employee.Email);
        }
    }
}