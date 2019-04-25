using System;
using System.ComponentModel.DataAnnotations;

namespace HRM.ERP.Models.HRM
{
    public class EmploymentStatus
    {
        [Key]

        public int Id { get; set; }

        public string StatusName { get; set; }

        public DateTime DateCreated { get; set; }
        
    }
}
