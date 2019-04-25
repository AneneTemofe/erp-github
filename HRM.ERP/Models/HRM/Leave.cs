using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.ERP.Models.HRM
{
    public class Leave : BaseClass 
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string LeaveTitle { get; set; }
        public string Description { get; set; }
        public DateTime LeaveStartDate { get; set; }
        public DateTime LeaveEndDate { get; set; }
        public DateTime LeaveApprovedDate { get; set; }

        public bool IsApproved { get; set; }
        public string Status { get; set; }

        public virtual Employee Employee { get; set; }

    }
}
