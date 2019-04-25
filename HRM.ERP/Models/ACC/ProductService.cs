using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.ERP.Models.ACC
{
    public class ProductService : BaseClass
    {
        public Guid Id { get; set; }
        public Guid OrganisationId { get; set; }
        public virtual Organisation Organisation { get; set; }

        public string Name { get; set; }
        public string  Description { get; set; }
        public float Price { get; set; }
        public float SalesTax { get; set; }
        public bool Sell { get; set; }
        public string Status { get; set; }
    }
}
