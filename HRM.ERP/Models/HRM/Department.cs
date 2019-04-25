using System;
using System.ComponentModel.DataAnnotations;

namespace HRM.ERP.Models.HRM
{
    public class Department : BaseClass
    {
        [Key]
        public Guid Id { get; set; }
    public Guid OrganisationId { get; set; }
    public string DepartmentName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
