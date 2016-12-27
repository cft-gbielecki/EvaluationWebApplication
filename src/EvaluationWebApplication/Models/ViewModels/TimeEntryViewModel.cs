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

        public TimeEntryViewModel(_RetrieveTimeEntryViewModel retrieveModel)
        {
            ConstructClass(context.Employees.FirstOrDefault(empl => retrieveModel.EmployeeId == empl.EmployeeID), retrieveModel.StartDate, retrieveModel.EndDate);
            RetrieveTimeEntry = new _RetrieveTimeEntryViewModel(retrieveModel.StartDate, retrieveModel.EndDate);
            if (retrieveModel.Contract != "ALL")
                RetrieveTimeEntry.Contract = context.Contracts.FirstOrDefault(contract => contract.ContractID.ToString() == retrieveModel.Contract).Contract;
            if (retrieveModel.Service != "ALL")
                RetrieveTimeEntry.Service = retrieveModel.Service;
            if (retrieveModel.Client != "ALL")
                RetrieveTimeEntry.Client = context.Clients.FirstOrDefault(client => client.ClientID.ToString() == retrieveModel.Client).ClientName;
            RetrieveTimeEntry.Employee = retrieveModel.Employee;
            FillRetrieveTimeEntry();
            //RetrieveTimeEntry. = retrieveModel;
            //if(retrieveModel.Employee == "ALL")

        }

        public TimeEntryViewModel(Employee employee) : this(employee, new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1), new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1).AddDays(-1))
        {
        }

        public TimeEntryViewModel(Employee employee, DateTime start, DateTime end)
        {
            ConstructClass(employee, start, end);
            RetrieveTimeEntry = new _RetrieveTimeEntryViewModel(start, end);
            FillRetrieveTimeEntry();
        }

        private void FillRetrieveTimeEntry()
        {
            //RetrieveTimeEntry.SetEmployee(Employee.FirstName, Employee.Surname);
            RetrieveTimeEntry.AllContracts = SetEmployeeContracts(RetrieveTimeEntry.AllContracts);
            RetrieveTimeEntry.AllEmployeeServices = SetEmployeeServices(RetrieveTimeEntry.AllEmployeeServices);
            RetrieveTimeEntry.AllClients = SetClients(RetrieveTimeEntry.AllClients);
            RetrieveTimeEntry.EmployeeId = Employee.EmployeeID;
            if (Employee.IsAdministrator == true)
                RetrieveTimeEntry.EmployeeList = GatherAllEmployees(RetrieveTimeEntry.EmployeeList);
            else
                RetrieveTimeEntry.EmployeeList = GatherEmployee(RetrieveTimeEntry.EmployeeList);
        }

        private void ConstructClass(Employee employee, DateTime start, DateTime end)
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
            TimeEntries = context.TimeEntries.Where(te => te.Date >= start && te.Date <= end).OrderBy(te => te.Date).ToList();
            Contracts = context.Contracts.Where(x => x.EmployeeID == Employee.EmployeeID).ToList();
            CreateTimeEntry = new _CreateTimeEntryViewModel();
            CreateTimeEntry.EmployeeContracts = new List<SelectListItem>();
            CreateTimeEntry.EmployeeContracts = SetEmployeeContracts(CreateTimeEntry.EmployeeContracts);
            CreateTimeEntry.EmployeeServices = new List<SelectListItem>();
            CreateTimeEntry.EmployeeServices = SetEmployeeServices(CreateTimeEntry.EmployeeServices);
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
                Value = string.Format("{0}, {1}", empl.FirstName, empl.Surname),
                Text = string.Format("{0}, {1}", empl.FirstName, empl.Surname)
            };
        }

        private List<SelectListItem> GatherEmployee(List<SelectListItem> employeeList)
        {
            employeeList.Add(new SelectListItem
            {
                Value = "ALL",
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
