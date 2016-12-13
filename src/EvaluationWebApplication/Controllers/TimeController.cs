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
        public IActionResult Create(TimeEntryViewModel timeModelCreate)
        {
            
            //string sasa = timeModelCreate.CreateTimeEntry.TimeEntryContract.Substring(0, timeModelCreate.CreateTimeEntry.TimeEntryContract.IndexOf(':') - 1);
            timeModel = new TimeEntryViewModel(timeModelCreate.Employee);


            TimeEntry timeEntry = new TimeEntry()
            {
                Date = timeModelCreate.CreateTimeEntry.TimeEntryDate,
                EmployeeName = timeModel.Employee.FirstName,
                EmployeeSurname = timeModel.Employee.Surname,
                Client = timeModel.Clients.FirstOrDefault(client => client.ClientID == timeModel.Contracts.FirstOrDefault(contract => timeModelCreate.CreateTimeEntry.TimeEntryContract == contract.ContractID.ToString()).ClientID).ClientName,
                    // timemode timeModelCreate.CreateTimeEntry.TimeEntryContract == client.ClientID.ToString()).ClientName,
                // timeModelCreate.CreateTimeEntry.TimeEntryContract.Substring(0, timeModelCreate.CreateTimeEntry.TimeEntryContract.IndexOf(':') - 1), //timeModelCreate.Clients.FirstOrDefault(client => client.ClientName == timeModelCreate.CreateTimeEntry.TimeEntryContract.Substring(0, timeModelCreate.CreateTimeEntry.TimeEntryContract.IndexOf(':') - 1)).ClientName,
                Comment = timeModelCreate.CreateTimeEntry.TimeEntryComment,
                Contract = timeModelCreate.CreateTimeEntry.TimeEntryContract,
                //Employee = timeModel.Employee,
                EmployeeID = timeModel.Employee.EmployeeID,
                MakeUp = timeModelCreate.CreateTimeEntry.TimeEntryMakeUp,
                Project = timeModelCreate.CreateTimeEntry.TimeEntryProject,
                Service = timeModelCreate.CreateTimeEntry.TimeEntryService,
                Time = timeModelCreate.CreateTimeEntry.TimeEntryTime
            };
            context.TimeEntries.Add(timeEntry);
            context.SaveChanges();

            ServiceClass serviceEntry = new ServiceClass(
                (EvaluationWebApplication.Models.CFT.Services)Enum.Parse(typeof(EvaluationWebApplication.Models.CFT.Services),
                            timeModelCreate.CreateTimeEntry.TimeEntryService));
            serviceEntry.EntryID = timeEntry.EntryID;
            context.Service.Add(serviceEntry);
            context.SaveChanges();

            return RedirectToAction("Index","Time",new { email = timeModelCreate.Employee.Email });
        }
    }
}