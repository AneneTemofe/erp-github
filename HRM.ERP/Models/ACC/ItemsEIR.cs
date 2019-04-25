using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.ERP.Models.ACC
{
    public class ItemsEIR : BaseClass
    {
        public Guid Id { get; set; }
        public Guid EstimateId { get; set; }
        public string Item { get; set; }
        public string  Description { get; set; }
        public float Quantity { get; set; }
        public float Price { get; set; }
        public float Discount { get; set; }
        public float Amount { get; set; }

        public virtual Estimate Estimate { get; set; }

    }
}
