using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.ERP.Models.HRM
{
    public class Payroll : BaseClass
    {
        [key]
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid DepartmentId { get; set; }
    public Guid OrganisationId { get; set; }
    public float  GrossSalary { get; set; }
        public float Tax { get; set; }
        public float Pension { get; set; }
        public float Credit { get; set; }
        public float Debit { get; set; }
    public string Month { get; set; }
    public string Year { get; set; }
        public virtual Employee Employee { get; set; }


    }
}
