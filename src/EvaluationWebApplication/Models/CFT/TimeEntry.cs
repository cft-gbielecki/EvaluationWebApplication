using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationWebApplication.Models.CFT
{
    public class TimeEntry
    {
        [Key]
        public int EntryID { get; set; }
        public DateTime Date { get; set; }
        public string Employee { get; set; }
        public string Client { get; set; }
        public string Contract { get; set; }
        public string Service { get; set; }
        public string Time { get; set; }
        public bool MakeUp { get; set; }
        public string Project { get; set; }
        public string Comment { get; set; }
    }
}
