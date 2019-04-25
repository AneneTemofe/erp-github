using System;

namespace HRM.ERP.Models
{
    public class SubOrdinates
    {
        public int id { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public int employeeId { get; set; }

        public string reportingMethod { get; set; }

        public DateTime dateCreated { get; set; } 
    }
}
