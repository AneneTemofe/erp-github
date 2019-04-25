using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.ERP.Models.HRM
{
    public class EmergencyContact
    {
        public int id { get; set; }

        public int employeeId { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string middleName { get; set; }

        public string telephoneNo { get; set; }

        public string email { get; set; }

        public string relationship { get; set; }

        public string mobileNo { get; set; }

        public DateTime dateCreated { get; set; }

    }
}
