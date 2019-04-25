using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRM.ERP.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using HRM.ERP.Data;

namespace HRM.ERP.Controllers
{
  [Authorize]
    public class HomeController : Controller
    {
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
      _context = context;
    }

    [Authorize]
        public IActionResult Index()
        {
      //return Redirect("/Admin/Dashboard");
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

      if (org == null)
      {
        ViewBag.IsOrg = "NotRegistered";
        return View();
      }

      ViewBag.IsOrg = "Registered";
            return View();

        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Organisation()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
    }
}
