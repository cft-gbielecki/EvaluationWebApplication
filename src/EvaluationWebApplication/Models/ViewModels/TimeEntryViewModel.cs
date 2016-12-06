using EvaluationWebApplication.Models.CFT;
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

        private CFTDbContext context = new CFTDbContext();
        public TimeEntryViewModel()
        {
            foreach(EvaluationWebApplication.Models.CFT.Services service in Enum.GetValues(typeof(EvaluationWebApplication.Models.CFT.Services)))
            {
                Services.Add(new ServiceClass(service));
            }
            Clients = context.Clients.ToList();
        }
    }



}
