using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.ERP.Models.HRM
{
    public class Dependants
    {
        public Guid Id { get; set; }

        public Guid OrganisationId { get; set; }

        public string DepartmentName { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public string relationship { get; set; }

        public DateTime dob { get; set; }

        public DateTime dateCreated { get; set; }

    }
}
