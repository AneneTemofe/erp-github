using System;
using System.ComponentModel.DataAnnotations;

namespace HRM.ERP.Models.HRM
{
    public class CreditDebit : BaseClass
    {
        [Key]
        public Guid Id { get; set;}
        public Guid EmployeeId { get; set; }
    public Guid OrganisationId { get; set; }
    public string CreditDebitType { get; set; }
    public string Description { get; set; }
    public float Amount { get; set; }

        public virtual Employee Employee { get; set; }


    }
}
