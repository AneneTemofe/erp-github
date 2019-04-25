using HRM.ERP.Models.ACC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.ERP.Models.ACCViewModel
{
  public class CustomerStatementViewModel
  {
    public Guid CustomerId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public Customer Customer { get; set; }
    public Organisation Organisation { get; set; }


    public float Balance { get; set; }

    public List<CustomerState> CustomerStates { get; set; }


  }

  public class CustomerState
  {
    public DateTime StateDate { get; set; }
    public string Details { get; set; }
    public float Amount { get; set; }
  }
}
