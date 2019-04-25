using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.ERP.Models.ACCViewModel
{
  public class ProfitLossViewModel
  {
    public float Income { get; set; }
    public float CostOfGoodSold { get; set; }
    public float OperationExpenses { get; set; }
    public float NetProfit { get; set; }
    public float GrossIncomePercentage { get; set; }
    public float NetIncomePercentage { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

  }
}
