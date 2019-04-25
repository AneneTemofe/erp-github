using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.ERP.Models.ACC
{
  public class Transaction : BaseClass
  {
    public Guid Id { get; set; }
    public Guid OrganisationId { get; set; }
    public string TransactionDescription { get; set; }
    public string TransactionType { get; set; }
    public float Amount { get; set; }
    public float Tax { get; set; }

  }
}
