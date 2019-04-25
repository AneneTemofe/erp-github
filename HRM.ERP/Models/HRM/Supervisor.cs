using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.ERP.Models.HRM
{
    public class Supervisor
    {
        public int id { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string reportingMethod { get; set; }

        public int supervisorId { get; set; }

        public int employeeId { get; set; }

        public DateTime dateCreated { get; set; }
    }
}
