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

        private CFTDbContext context = new CFTDbContext();
        public TimeEntryViewModel(Employee employee)
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
            TimeEntries = context.TimeEntries.ToList();
            Contracts = context.Contracts.Where(x => x.EmployeeID == Employee.EmployeeID).ToList();
            CreateTimeEntry = new _CreateTimeEntryViewModel();
            CreateTimeEntry.EmployeeContracts = new List<SelectListItem>();
            SetEmployeeContracts();
            CreateTimeEntry.EmployeeServices = new List<SelectListItem>();
            SetEmployeeServices();
        }

        private void SetEmployeeServices()
        {
            if (Services != null)
            {
                foreach(ServiceClass service in Services)
                {
                    CreateTimeEntry.EmployeeServices.Add(new SelectListItem {
                        Text = String.Format("{0} {1}", service.ServiceSuffix, service.ServiceType.ToString()),
                        Value = service.ServiceType.ToString()
                    });
                }
            }
        }

        private void SetEmployeeContracts()
        {
            if (Contracts != null)
            {
                foreach (Contracts contract in Contracts)
                {
                    if (contract.DateStart >= DateTime.Now && contract.DateFinish <= DateTime.Now)
                    {
                        string contract_string = String.Format("{0}: {1} ({2} - {3})",
                            Clients.FirstOrDefault(client => client.ClientID == contract.ClientID).ClientName, contract.Contract, contract.DateStart.Date.ToString(), contract.DateFinish.Date);
                        CreateTimeEntry.EmployeeContracts.Add(new SelectListItem { Text = contract_string, Value = contract.ContractID.ToString() });
                    }
                }
            }
        }
    }



}
