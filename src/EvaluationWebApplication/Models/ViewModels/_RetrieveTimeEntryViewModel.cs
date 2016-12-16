﻿using Microsoft.AspNetCore.Mvc.Rendering;
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


        public _RetrieveTimeEntryViewModel()
        {
            CommonConstruction();
            StartDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            EndDate = StartDate.AddMonths(1).AddDays(-1);
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
            AllContracts.Add(new SelectListItem() { Value = "0", Text = "ALL" });
            AllEmployeeServices.Add(new SelectListItem() { Value = "0", Text = "ALL" });
            AllClients.Add(new SelectListItem() { Value = "0", Text = "ALL" });
            EmployeeList = new List<SelectListItem>();
        }

    }
}