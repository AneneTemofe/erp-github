using System;
using System.ComponentModel.DataAnnotations;

namespace HRM.ERP.Models.HRM
{
    public class JobTitle : BaseClass
    {
        [Key]
        public Guid Id { get; set; }
        public Guid OrganisationId { get; set; }
        public string TitleName { get; set; }
        public string Description { get; set; }

    }
}
