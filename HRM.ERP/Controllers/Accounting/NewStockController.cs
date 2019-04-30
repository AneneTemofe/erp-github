using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HRM.ERP.Controllers.Accounting
{
    public class NewStockController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AvailableStock()
        {
            return View();
        }

        public IActionResult History()
        {
            return View();
        }
    }
}