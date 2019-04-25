using System;
using System.ComponentModel.DataAnnotations;

namespace HRM.ERP.Models.HRM
{
    public class PayGrade : BaseClass
    {
        [Key]
        public Guid Id { get; set;}
        public Guid JobId { get; set; }
    public Guid OrganisationId { get; set; }
    public string GradeName { get; set; }
        public double Price { get; set; }

        public virtual Job Job { get; set; }


    }
}
