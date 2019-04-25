using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.ERP.Models.ACC
{
  public class Receipt : BaseClass
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ReceiptNo { get; set; }
    public string ImageUrl { get; set; }
    public Guid OrganisationId { get; set; }
    
  }
}
