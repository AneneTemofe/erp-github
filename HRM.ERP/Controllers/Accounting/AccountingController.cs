using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HRM.ERP.Controllers.Accounting
{
    public class AccountingController : Controller
    {
        public IActionResult TransactionsIndex()
        {
            return View();
        }
        public IActionResult ReconciliationIndex()
        {
          return View();
        }
        public IActionResult ChartOfAccountsIndex()
        {
          return View();
        }
        [Route("ACC/Dashboard")]
        public IActionResult AccountDashboard()
        {
          return View();
        }
        public IActionResult Addincome()
        {
          return View();
        }
        public IActionResult Addexpense()
        {
          return View();
        }
    }
}