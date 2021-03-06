﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.ERP.Models.ACC
{
    public class Vendor : BaseClass
    {
        public Guid Id { get; set; }
        public Guid OrganisationId { get; set; }
        public string VendorName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string  State { get; set; }
        public string  Country { get; set; }

    }
}
