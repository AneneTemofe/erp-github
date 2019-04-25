using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HRM.ERP.Data;
using HRM.ERP.Models;
using HRM.ERP.Models.ACCViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HRM.ERP.Controllers.Accounting
{
    public class ReportController : Controller
    {
    private readonly ApplicationDbContext _context;

    public ReportController(ApplicationDbContext context)
    {
      _context = context;
    }
    public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProfitandLoss(int year, int quater, int month, DateTime startdate, DateTime enddate)
        {
      var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
      Organisation org;
      var otheruser = _context.OtherUsers.Where(x => x.UserId == userId).FirstOrDefault();
      if (otheruser == null)
      {
        org = _context.Organisation.Where(m => m.ApplicationUserId == userId).FirstOrDefault();

      }
      else
      {
        org = _context.Organisation.Where(m => m.ApplicationUserId == otheruser.HostId).FirstOrDefault();

      }

      ProfitLossViewModel plVM = new ProfitLossViewModel();

      if (year == 0)
      {
        var income = _context.Estimates.Where(x => x.OrganisationId == org.Id).Where(x => x.DateCreated.Year == DateTime.Now.Year).Where(x => x.PaymentStatus == "PAID").Select(x => x.GrandTotal).Sum();
        var totalIncome = _context.Transactions.Where(x => x.OrganisationId == org.Id).Where(x => x.DateCreated.Year == DateTime.Now.Year).Where(x => x.TransactionType == "Income").Select(x => x.Amount).Sum();
        plVM.Income = totalIncome;

        var totalExpenses = _context.Transactions.Where(x => x.OrganisationId == org.Id).Where(x => x.DateCreated.Year == DateTime.Now.Year).Where(x => x.TransactionType == "Expenses").Select(x => x.Amount).Sum();
        plVM.OperationExpenses = totalExpenses;

        plVM.StartDate = DateTime.Parse("01/01/" + DateTime.Now.Year.ToString());
        plVM.EndDate = DateTime.Today;


        plVM.NetProfit = totalIncome - totalExpenses;

        plVM.NetIncomePercentage = (totalIncome - totalExpenses) * 100 / totalIncome;

        return View(plVM);

      }
      else
      {

      }


            return View();
        }

        public IActionResult BalanceSheet()
        {
            return View();
        }

        public IActionResult CashFlow()
        {
            return View();
        }

        public IActionResult AccountBalance()
        {
            return View();
        }
    }
}


