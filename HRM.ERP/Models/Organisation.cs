using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.ERP.Models
{
    public class Organisation : BaseClass
    {
        public Guid Id { get; set; }
        public Guid ApplicationUserId { get; set; }

        public string OrganisationName { get; set; }

        public int NoOfEmployees { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public int ZipCode { get; set; }

        public string TaxId { get; set; }

        public string RegistrationNo { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
