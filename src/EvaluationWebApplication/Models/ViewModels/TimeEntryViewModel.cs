using EvaluationWebApplication.Models.CFT;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationWebApplication.Models.ViewModels
{
    public class TimeEntryViewModel
    {
        public Employee Employee { get; set; }
        public List<TimeEntry> TimeEntries { get; set; }
        public List<Clients> Clients { get; set; }
        public List<ServiceClass> Services { get; set; }
        public List<Contracts> Contracts { get; set; }
        public _CreateTimeEntryViewModel CreateTimeEntry { get; set; }
        public _RetrieveTimeEntryViewModel RetrieveTimeEntry { get; set; }

        private CFTDbContext context = new CFTDbContext();

        public TimeEntryViewModel() { }

        public TimeEntryViewModel(Employee employee, _RetrieveTimeEntryViewModel retrieveModel)
        { }

        public TimeEntryViewModel(Employee employee) : this(employee, new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1), new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1).AddDays(-1))
        {
            //DateTime firstDayOfThisMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            //DateTime lastDayOfThisMonth = firstDayOfThisMonth.AddMonths(1).AddDays(-1);
            //Employee = employee;
            //Services = new List<ServiceClass>();
            //Clients = new List<Clients>();
            //TimeEntries = new List<TimeEntry>();
            //foreach (EvaluationWebApplication.Models.CFT.Services service in Enum.GetValues(typeof(EvaluationWebApplication.Models.CFT.Services)))
            //{
            //    Services.Add(new ServiceClass(service));
            //}
            //Clients = context.Clients.ToList();
            //TimeEntries = context.TimeEntries.Where(te => te.Date > firstDayOfThisMonth && te.Date < lastDayOfThisMonth).ToList();
            //Contracts = context.Contracts.Where(x => x.EmployeeID == Employee.EmployeeID).ToList();
            //CreateTimeEntry = new _CreateTimeEntryViewModel();
            //CreateTimeEntry.EmployeeContracts = new List<SelectListItem>();
            //CreateTimeEntry.EmployeeContracts = SetEmployeeContracts(CreateTimeEntry.EmployeeContracts);
            //CreateTimeEntry.EmployeeServices = new List<SelectListItem>();
            //CreateTimeEntry.EmployeeServices = SetEmployeeServices(CreateTimeEntry.EmployeeServices);
            //RetrieveTimeEntry = new _RetrieveTimeEntryViewModel(firstDayOfThisMonth, lastDayOfThisMonth);
            //FillRetrieveTimeEntry();
        }

        public TimeEntryViewModel(Employee employee, DateTime start, DateTime end)
        {
            Employee = employee;
            Services = new List<ServiceClass>();
            Clients = new List<Clients>();
            TimeEntries = new List<TimeEntry>();
            foreach (EvaluationWebApplication.Models.CFT.Services service in Enum.GetValues(typeof(EvaluationWebApplication.Models.CFT.Services)))
            {
                Services.Add(new ServiceClass(service));
            }
            Clients = context.Clients.ToList();
            TimeEntries = context.TimeEntries.Where(te => te.Date > start && te.Date < end).ToList();
            Contracts = context.Contracts.Where(x => x.EmployeeID == Employee.EmployeeID).ToList();
            CreateTimeEntry = new _CreateTimeEntryViewModel();
            CreateTimeEntry.EmployeeContracts = new List<SelectListItem>();
            CreateTimeEntry.EmployeeContracts = SetEmployeeContracts(CreateTimeEntry.EmployeeContracts);
            CreateTimeEntry.EmployeeServices = new List<SelectListItem>();
            CreateTimeEntry.EmployeeServices = SetEmployeeServices(CreateTimeEntry.EmployeeServices);
            RetrieveTimeEntry = new _RetrieveTimeEntryViewModel(start, end);
            FillRetrieveTimeEntry();
        }

        private void FillRetrieveTimeEntry()
        {
            //RetrieveTimeEntry.SetEmployee(Employee.FirstName, Employee.Surname);
            RetrieveTimeEntry.AllContracts = SetEmployeeContracts(RetrieveTimeEntry.AllContracts);
            RetrieveTimeEntry.AllEmployeeServices = SetEmployeeServices(RetrieveTimeEntry.AllEmployeeServices);
            RetrieveTimeEntry.AllClients = SetClients(RetrieveTimeEntry.AllClients);
            if (Employee.IsAdministrator == true)
                GatherAllEmployees(RetrieveTimeEntry.EmployeeList);
            else
                GatherEmployee(RetrieveTimeEntry.EmployeeList);
        }

        private List<SelectListItem> GatherAllEmployees(List<SelectListItem> employeeList)
        {
            employeeList = GatherEmployee(employeeList);
            foreach (Employee empl in context.Employees)
                employeeList.Add(EmployeeListItem(empl));
            return employeeList;
        }

        private SelectListItem EmployeeListItem(Employee empl)
        {
            return new SelectListItem()
            {
                Value = empl.EmployeeID.ToString(),
                Text = string.Format("{0}, {1}", empl.FirstName, empl.Surname)
            };
        }

        private List<SelectListItem> GatherEmployee(List<SelectListItem> employeeList)
        {
            employeeList.Add(new SelectListItem
            {
                Value = "0",
                Text = "ALL"
            });
            if (!Employee.IsAdministrator)
                employeeList.Add(EmployeeListItem(Employee));
            return employeeList;
        }

        private List<SelectListItem> SetEmployeeServices(List<SelectListItem> serviceList)
        {
            if (Services != null)
            {
                foreach (ServiceClass service in Services)
                {
                    serviceList.Add(new SelectListItem
                    {
                        Text = String.Format("{0} {1}", service.ServiceSuffix, service.ServiceType.ToString()),
                        Value = service.ServiceType.ToString()
                    });
                }
            }
            return serviceList;
        }

        private List<SelectListItem> SetEmployeeContracts(List<SelectListItem> contractList)
        {
            if (Contracts != null)
            {
                foreach (Contracts contract in Contracts)
                {
                    if (contract.DateStart <= DateTime.Now && contract.DateFinish >= DateTime.Now)
                    {
                        string contract_string = String.Format("{0}: {1} ({2} - {3})",
                            Clients.FirstOrDefault(client => client.ClientID == contract.ClientID).ClientName, 
                            contract.Contract, 
                            Convert.ToString(string.Format("{0:dd/MM/yyyy}", contract.DateStart.Date)),
                            Convert.ToString(string.Format("{0:dd/MM/yyyy}", contract.DateFinish.Date)));
                        contractList.Add(new SelectListItem { Text = contract_string, Value = contract.ContractID.ToString() });
                    }
                }
            }
            return contractList;
        }
        private List<SelectListItem> SetClients(List<SelectListItem> clientList)
        {
            if (Clients != null)
            {
                foreach (Clients client in Clients)
                {
                    clientList.Add(new SelectListItem { Text = client.ClientName, Value = client.ClientID.ToString() });

                }
            }
            return clientList;
        }


    }



}
