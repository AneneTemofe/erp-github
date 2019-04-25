using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.ERP.Models.ACC
{
    public class Bill : BaseClass
    {

        public Guid Id { get; set; }
        public Guid VendorId { get; set; }
        public Guid OrganisationId { get; set; }
        public string BillNote { get; set; }
        public string  BillId { get; set; }
        public int BillNo { get; set; }

        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }

        public string POSO { get; set; }

        public float Total { get; set; }
        public float GrandTotal { get; set; }
        public string Status { get; set; }


        public virtual Vendor Vendor { get; set; }

    }
}
