using HRM.ERP.Models.HRM;
using HRM.ERP.Models.ACC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.ERP.Models.ACCViewModel
{
    public class QuoteViewModel
    {

        //public IEnumerable<Job> Jobs { get; set; }
        public Guid Id { get; set; }
        public Guid EstimateId { get; set; }
        //public List<>
        public Organisation Organisation { get; set; }
        public Guid CustomerId { get; set; }
        public List<ItemsEIR> ItemsEIRs { get; set; }

        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }

        public string SubTotal { get; set; }
        public float Vat { get; set; }
        public float Percentage { get; set; }
        public string Total { get; set; }
        public string Status { get; set; }
        public int QuoteNo { get; set; }
      
        
        

    }
}
