using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EvaluationWebApplication.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        //[Key]
        //public int ID { get; set; }
        //public int EmployeeID { get; set; }
        //public string Name { get; set; }
    }
}
