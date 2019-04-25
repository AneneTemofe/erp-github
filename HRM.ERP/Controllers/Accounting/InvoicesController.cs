using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HRM.ERP.Data;
using HRM.ERP.Models;
using HRM.ERP.Models.ACC;
using HRM.ERP.Models.ACCViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HRM.ERP.Controllers.Accounting
{
    public class InvoicesController : Controller
    {

    private readonly ApplicationDbContext _context;

    public InvoicesController(ApplicationDbContext context)
    {
      _context = context;
    }

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

      //var userID = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
      //var Organisation = _context.Organisation.Where(x => x.ApplicationUserId == userID).FirstOrDefault();

      var applicationDbContext = _context.Estimates.Include(e => e.Customer).Where(x => x.Status == "INVOICE").Where(x => x.OrganisationId == org.Id);
      return View(await applicationDbContext.OrderByDescending(x => x.EstimateNo).ToListAsync());

        }

    public async Task<IActionResult> AddInvoice(Guid Id)
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

        int quoteCount = _context.Estimates.Where(x => x.OrganisationId == org.Id).Count();

        Id = Guid.NewGuid();

        Estimate newEstimate = new Estimate()
        {
          Id = Id,
          EstimateDate = DateTime.Now,
          ExpireDate = DateTime.Now,
          OrganisationId = org.Id,
          Status = "INVOICE",
          CustomerId = customer.Id,
          EstimateNo = quoteCount + 1,


        };
        _context.Add(newEstimate);
        _context.SaveChanges();

        return Redirect("/Invoices/AddInvoice/?id=" + Id.ToString());

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
    public ActionResult AddInvoice(QuoteViewModel qVM)
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


    public async Task<IActionResult> InvoicePaid(Guid Id)
    {
      var estm = _context.Estimates.Where(x => x.Id == Id).FirstOrDefault();
      estm.PaymentStatus = "PAID";

      _context.Update(estm);
      _context.SaveChanges();

      return RedirectToAction(nameof(Index));

    }

    [HttpPost]
    public async Task<IActionResult> UploadImage(FileUpload fileDetails)
    {
      IFormFile rawFile = fileDetails.UploadFile;

      if (fileDetails.UploadFile == null || fileDetails.UploadFile.Length == 0)
        return Content("file not selected");

      string filename = Guid.NewGuid().ToString() + rawFile.FileName;
      
     // var path = Path.Combine( Directory.GetCurrentDirectory(), "wwwroot", rawFile.GetFilename());
      var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadedImages", filename);

      using (var stream = new FileStream(imagePath, FileMode.Create))
      {
        await rawFile.CopyToAsync(stream);


      }

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

      FileUploads newFile = new FileUploads()
      {
        OrganisationId = org.Id,
        FileName = filename,
        Amount = fileDetails.Amount,
        Description = fileDetails.Description,
        UploadType = "PAID INVOICE",
        Title = fileDetails.Title,
        Id = Guid.NewGuid()
      };

      _context.FileUpload.Add(newFile);
      _context.SaveChanges();

      var estm = _context.Estimates.Where(x => x.Id == fileDetails.Id).FirstOrDefault();
      estm.PaymentStatus = "PAID";

      _context.Update(estm);
      _context.SaveChanges();

      Transaction newTrans = new Transaction()
      {
        Id = Guid.NewGuid(),
        Amount = estm.Total,
        TransactionDescription = "Payment Invoice " + estm.EstimateNo,
        TransactionType = "Income",
        Tax = 0,
        OrganisationId = org.Id
      };

      _context.Add(newTrans);
      _context.SaveChanges();



      //var estm = _context.Estimates.Where(x => x.Id == fileDetails.Id).FirstOrDefault();
      //estm.PaymentStatus = "PAID";

      //_context.Update(estm);
      //_context.SaveChanges();


      return RedirectToAction(nameof(Index));
    }


  }

  public class FileUpload
  {
    public string Title { get; set; }
    public IFormFile UploadFile { get; set; }
    public float Amount { get; set; }
    public string Description { get; set; }
    public Guid Id { get; set; }

  }


  }