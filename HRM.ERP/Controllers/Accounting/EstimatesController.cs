using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HRM.ERP.Data;
using HRM.ERP.Models;
using HRM.ERP.Models.ACC;
using HRM.ERP.Models.ACCViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;

namespace HRM.ERP.Controllers.Accounting
{
  [Authorize]
  public class EstimatesController : Controller
  {
    private readonly ApplicationDbContext _context;

    public EstimatesController(ApplicationDbContext context)
    {
      _context = context;
    }
    [Route("/ACC/Quotation")]
    public async Task<IActionResult> Index()
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

      var applicationDbContext = _context.Estimates.Where(x => x.OrganisationId == org.Id).Include(e => e.Customer);
      return View(await applicationDbContext.Where(x => x.Status == null).ToListAsync());
    }

    [Route("/ACC/Quotation/AddQuot")]
    public async Task<IActionResult> AddEstimate(Guid Id)
    {
      QuoteViewModel qVM = new QuoteViewModel();

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

      var userID = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
      qVM.Organisation = _context.Organisation.Where(x => x.Id == org.Id).FirstOrDefault();


      if (Id == Guid.Empty)
      {
        Customer newCustomer = new Customer();
        newCustomer.OrganisationId = org.Id;

        var customer = _context.Customers.Where(x => x.OrganisationId == org.Id).FirstOrDefault();
        if (customer == null)
        {
          _context.Add(newCustomer);
          _context.SaveChanges();

          customer = newCustomer;
        }

        Id = Guid.NewGuid();

        int quoteCount = _context.Estimates.Where(x => x.OrganisationId == org.Id).Count();


        Estimate newEstimate = new Estimate()
        {
          Id = Id,
          EstimateDate = DateTime.Now,
          ExpireDate = DateTime.Now,
          OrganisationId = org.Id,
          CustomerId = customer.Id,
          EstimateNo = quoteCount + 1,

          

        };
        _context.Add(newEstimate);
        _context.SaveChanges();

        return Redirect("/ACC/Quotation/AddQuot/?id=" + Id.ToString());

      }
      var estm = _context.Estimates.Where(x => x.Id == Id).FirstOrDefault();
      qVM.ItemsEIRs = _context.ItemsEIRs.Where(x => x.EstimateId == Id).ToList();
      qVM.EstimateId = Id;
      qVM.SubTotal = (qVM.ItemsEIRs.Select(x => x.Amount).Sum()).ToString("N");
      qVM.Total = (qVM.ItemsEIRs.Select(x => x.Amount).Sum() * (1 + qVM.Vat / 100)).ToString("N");
      qVM.DueDate = estm.ExpireDate;
      qVM.InvoiceDate = estm.EstimateDate;

      qVM.QuoteNo = estm.EstimateNo;

      ViewData["CustomerId"] = new SelectList(_context.Set<Customer>().Where(x => x.OrganisationId == org.Id), "Id", "CustomerName");

      //.ToString("C3", CultureInfo.CreateSpecificCulture("da-DK")
      return View(qVM);
    }

    [HttpPost]
    [Route("/ACC/Quotation/AddQuot")]
    public ActionResult AddEstimate(QuoteViewModel qVM)
    {
      var itemEIRs = _context.ItemsEIRs.Where(x => x.EstimateId == qVM.Id).ToList();
      Guid estimateId = itemEIRs.FirstOrDefault().EstimateId;
      Estimate theEstimate = _context.Estimates.Where(x => x.Id == estimateId).FirstOrDefault();

      theEstimate.CustomerId = qVM.CustomerId;
      theEstimate.EstimateDate = qVM.InvoiceDate;
      theEstimate.ExpireDate = qVM.DueDate;
      theEstimate.Total = itemEIRs.Select(x => x.Amount).Sum();
      theEstimate.GrandTotal = itemEIRs.Select(x => x.Amount).Sum() * (1 + qVM.Vat / 100);

      _context.Update(theEstimate);
      _context.SaveChanges();

      return RedirectToAction(nameof(Index));
    }

    public ActionResult ConvertInvoice(Guid Id)
    {
      Estimate theEstimate = _context.Estimates.Where(x => x.Id == Id).FirstOrDefault();
      theEstimate.Status = "INVOICE";

      _context.Update(theEstimate);
      _context.SaveChanges();

      return RedirectToAction(nameof(Index));

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddItemEIR([Bind("Id,EstimateId,Item,Description,Quantity,Price,Discount,Amount,DateCreated,DateUpdated,DateModified,IsDeleted")] ItemsEIR itemsEIR)
    {
      if (ModelState.IsValid)
      {
        itemsEIR.Id = Guid.NewGuid();
        _context.Add(itemsEIR);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      //ViewData["EstimateId"] = new SelectList(_context.Estimates, "Id", "Id", itemsEIR.EstimateId);
      return View();
    }
    [HttpPost]
    public ActionResult DeleteItem(Guid id)
    {
      var itemsEIR = _context.ItemsEIRs.SingleOrDefaultAsync(m => m.Id == id);
      _context.Remove(itemsEIR);
      _context.SaveChangesAsync();

      return Json(new
      {
        msg = "Successfully deleted."
      });

    }

    [HttpPost]
    public ActionResult AddItem([FromBody]Items Item)
    {
      ItemsEIR singleItem = new ItemsEIR()
      {
        Id = Guid.NewGuid(),
        EstimateId = Item.EstimateId,
        Item = Item.Item,
        Description = Item.Description,
        Quantity = float.Parse(Item.Quantity),
        Price = float.Parse(Item.UnitCost),
        Amount = float.Parse(Item.Quantity) * float.Parse(Item.UnitCost),

      };

      _context.Add(singleItem);
      _context.SaveChanges();

      return Json(new
      {
        msg = "Successfully added."
      });


    }

  }


  public class Items
  {
    //public Guid EstimateId { get; set; }
    public Guid EstimateId { get; set; }
    public string Item { get; set; }
    public string Description { get; set; }
    public string Quantity { get; set; }
    public string UnitCost { get; set; }
    public string Amount { get; set; }
  }
}