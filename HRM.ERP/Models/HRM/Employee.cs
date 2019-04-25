using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace HRM.ERP.Models.HRM
{
    public class Employee : BaseClass
    {
        [Key]
        public Guid Id { get; set; }
        public Guid JobId { get; set; }
    public Guid OrganisationId { get; set; }
    public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string EmployeeId { get; set; }

        public string Photograph { get; set; }

        public string Status { get; set; }

        public DateTime DOB { get; set; }

        public string Nationality { get; set; }

        public string Gender { get; set; }

        public string MaritalStatus { get; set; }


        public string Address { get; set; }

        [MinLength(10, ErrorMessage = "The lenght character must not be less than 10 digits ")]
        public string MobileNo { get; set; }

        public string WorkTelephone { get; set; }

        public string Email { get; set; }

        [MinLength(10, ErrorMessage = "The lenght character must not be less than 10 digits ")]
        public string AccountNo { get; set; }

        public string AccountType { get; set; }

        public string SortCode { get; set; }

        public string contractType { get; set; }

        public DateTime JoinDate { get; set; }


        public virtual Job Job { get; set; }

    }
}
