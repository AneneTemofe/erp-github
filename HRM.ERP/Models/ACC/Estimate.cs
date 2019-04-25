using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.ERP.Models.ACC
{
    public class Estimate : BaseClass
    {

        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid OrganisationId { get; set; }

        public string EstimateTitle { get; set; }
        public string  EstimateId { get; set; }
    public int EstimateNo { get; set; }

        public DateTime EstimateDate { get; set; }
        public DateTime ExpireDate { get; set; }

        public string POSO { get; set; }
        public string Subheading { get; set; }
        public string Footer { get; set; }
        public string Memo { get; set; }


        public float Total { get; set; }
        public float GrandTotal { get; set; }
        public string Status { get; set; }
        public string PaymentStatus { get; set; }


        public virtual Customer Customer { get; set; }

    }
}
