using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationWebApplication.Models.ViewModels
{
    public class _RetrieveTimeEntryViewModel
    {
        public List<SelectListItem> AllContracts { get; set; }
        public List<SelectListItem> AllEmployeeServices { get; set; }
        public List<SelectListItem> AllClients { get; set; }
        public List<SelectListItem> EmployeeList { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Contract { get; set; }
        public string Service { get; set; }
        public string Client { get; set; }
        public string Employee { get; set; }
        public int EmployeeId { get; set; }


        public _RetrieveTimeEntryViewModel()
        {
            CommonConstruction();
            StartDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            EndDate = StartDate.AddMonths(1).AddDays(-1);
            Contract = "ALL";
            Service = "ALL";
            Client = "ALL";
            Employee = "ALL";
        }

        public _RetrieveTimeEntryViewModel(DateTime start, DateTime end)
        {
            CommonConstruction();
            StartDate = start;
            EndDate = end;
        }
        //public void SetEmployee(string name, string surname)
        //{
        //    Employee = String.Format("{0}, {1}", surname, name);
        //}

        private void CommonConstruction()
        {
            AllContracts = new List<SelectListItem>();
            AllEmployeeServices = new List<SelectListItem>();
            AllClients = new List<SelectListItem>();
            AllContracts.Add(new SelectListItem() { Value = "ALL", Text = "ALL" });
            AllEmployeeServices.Add(new SelectListItem() { Value = "ALL", Text = "ALL" });
            AllClients.Add(new SelectListItem() { Value = "ALL", Text = "ALL" });
            EmployeeList = new List<SelectListItem>();
        }

    }
}
