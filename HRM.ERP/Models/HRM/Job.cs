using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.ERP.Models.HRM
{
    public class Job : BaseClass
    {
        [key]
        public Guid Id { get; set; }
        public Guid JobTitleId { get; set; }
        public Guid ContractTypeId { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid OrganisationId { get; set; }
        public DateTime  ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
        public string JobDescription { get; set; }
        public virtual JobTitle JobTitle { get; set; }
        public virtual ContractType ContractType { get; set; }
        public virtual Department Department { get; set; }


    }
}
