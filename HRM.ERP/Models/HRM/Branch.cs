using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.ERP.Models.HRM
{
    public class Branch : BaseClass
    {
        public Guid Id { get; set; }
    public Guid OrganisationId { get; set;}

        public string BranchName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public int ZipCode { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string Status { get; set; }
    }
}
