using System;
using System.ComponentModel.DataAnnotations;

namespace HRM.ERP.Models.HRM
{
    public class ContractType : BaseClass
    {
        [Key]
        public Guid Id { get; set; }
    public Guid OrganisationId { get; set; }
    public string Name { get; set; }
        public string Description { get; set; } 
        public string Status { get; set; }
    }
}
