using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationWebApplication.Models.CFT
{
    public class Contracts
    {
        [Key]
        public int ContractID { get; set; }
        [ForeignKey("ClientId")]
        public int ClientID { get; set; }
        [ForeignKey("EmployeeId")]
        public int EmployeeID { get; set; }
        public string Contract { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateFinish { get; set; }

        public virtual Clients Client { get; set; }

        public virtual Employee Employee { get; set; }

    }
}
