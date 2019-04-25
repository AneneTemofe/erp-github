using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.ERP.Models.HRM
{
    public class Education : BaseClass 
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string Institution { get; set; }
        public string Year { get; set; }
        public string QualificationObtained{ get; set; }
        public string Grade { get; set; }

        public virtual Employee Employee { get; set; }


    }
}
