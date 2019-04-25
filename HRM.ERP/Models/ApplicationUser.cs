using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HRM.ERP.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string UserRole { get; set; }
        public string EmployeeName { get; set; }
        public string Usernames { get; set; }
        public string Status { get; set; }
    
     
        //public string Password { get; set; }
        //public string ComfirmPassword { get; set; }
    }
}
