using HRM.ERP.Models.HRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.ERP.Models.HRMViewModel
{
    public class DashboardViewModel
    {

        //public IEnumerable<Job> Jobs { get; set; }
        public int Employees { get; set; }

        public int Users { get; set; }

        public int Vacancies { get; set; }
        public int LeaveRequest { get; set; }

        public List<Employee> EmployeeList { get; set; }
        public List<ApplicationUser> UserList { get; set; }



    }
}
