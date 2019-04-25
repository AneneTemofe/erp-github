using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.ERP.Models.HRM
{
    public class Salary
    {
        public int id { get; set; }

        public string payGrade { get; set; }

        public int salaryComponent { get; set; }

        public int amount { get; set; }

        public string comment { get; set; }

        public string payFrequency { get; set; }

        public string payGradeId { get; set; }

        public DateTime dateCreated { get; set; }
    }
}
