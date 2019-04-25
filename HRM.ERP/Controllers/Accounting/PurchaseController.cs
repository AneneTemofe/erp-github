using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HRM.ERP.Data;
using HRM.ERP.Models;
using HRM.ERP.Models.ACC;
using HRM.ERP.Models.ACCViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HRM.ERP.Controllers.Accounting
{
    public class PurchaseController : Controller
    {
    private readonly ApplicationDbContext _context;

    public PurchaseController(ApplicationDbContext context)
    {
      _context = context;
    }
    public IActionResult BillsIndex()
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


      return View(_context.Bill.Where(x => x.OrganisationId == org.Id).Include(x => x.Vendor).ToList());
        }
        public IActionResult AddBills()
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


      //QuoteViewModel qVM = new QuoteViewModel();
      //qVM.Organisation = org;

      //qVM.ItemsEIRs = new List<ItemsEIR>();

      ViewData["VendorId"] = new SelectList(_context.Set<Vendor>().Where(x => x.OrganisationId == org.Id), "Id", "VendorName");

      Bill newBill = new Bill();
      return View(newBill);
        }
    [HttpPost]
    public IActionResult AddBills(Bill bill)
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

      bill.OrganisationId = org.Id;
      bill.Id = Guid.NewGuid();


      _context.Add(bill);
      _context.SaveChanges();


      return RedirectToAction(nameof(BillsIndex));
    }

    public IActionResult Approve(Guid Id)
    { 
      var bill = _context.Bill.Where(x => x.Id == Id).FirstOrDefault();

      bill.Status = "Approved";

      _context.Update(bill);
      _context.SaveChanges();

      Transaction newTrans = new Transaction()
      {
        Id = Guid.NewGuid(),
        Amount = bill.Total,
        TransactionDescription = "Bills Payment - " + bill.BillNote,
        TransactionType = "Expenses",
        Tax = 0,
        OrganisationId = bill.OrganisationId
      };

      _context.Add(newTrans);
      _context.SaveChanges();

      return RedirectToAction(nameof(BillsIndex));

    }

    public IActionResult ReceiptsIndex()
        {
      
          return View();
        }
        public IActionResult AddReceipts()
        {
          return View();
        }
        public IActionResult VendorsIndex()
        {
          return View();
        }
    public IActionResult AddVendors()
    {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddVendors([Bind("Id,OrganisationId,VendorName,Email,FirstName,LastName,Phone,Address,City,ZipCode,State,Country,DateCreated,DateUpdated,DateModified,IsDeleted")] Vendor vendor)
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

      if (ModelState.IsValid)
      {
        vendor.OrganisationId = org.Id;
        vendor.Id = Guid.NewGuid();
        _context.Add(vendor);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Vendors));
      }
      return View(vendor);
    }

    public IActionResult ProductServicesIndex()
        {
          return View();
        }
        public IActionResult AddProductServices()
        {
          return View();
        }

        public async Task<IActionResult> Vendors()
    {
      return View(await _context.Vendor.ToListAsync());
    }

    public IActionResult UploadImage(Receipt receipt)
    {

      return View(); 
    }

  }
}