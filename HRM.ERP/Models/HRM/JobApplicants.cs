using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.ERP.Models.HRM
{
    public class JobApplicants
    {
        public int id { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string middleName { get; set; }

        public string email { get; set; }

        public string contactNo { get; set; }

        public string vacancyTitle { get; set; }

        public string status { get; set; }

        public string resume { get; set; }

        public string comments { get; set; }

        public DateTime dateCreated { get; set; }

    }
}
