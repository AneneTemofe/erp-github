using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRM.ERP.Data;
using HRM.ERP.Models.ACC;
using HRM.ERP.Models;
using System.Security.Claims;
using HRM.ERP.Models.ACCViewModel;

namespace HRM.ERP.Controllers.Accounting
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Customers
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

      var applicationDbContext = _context.Customers.Where(x => x.OrganisationId == org.Id).Include(c => c.Organisation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.Organisation)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            ViewData["OrganisationId"] = new SelectList(_context.Set<Organisation>(), "Id", "Id");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerName,Email,Phone,Address,City,ZipCode,State,Country,AccountNumber,BankName,AccountName,SortCode,Website,Mobile,Note,Status,OrganisationId,DateCreated,DateUpdated,DateModified,IsDeleted")] Customer customer)
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


            customer.OrganisationId = org.Id;


            if (ModelState.IsValid)
            {
                customer.Id = Guid.NewGuid();
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrganisationId"] = new SelectList(_context.Set<Organisation>(), "Id", "Id", customer.OrganisationId);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.SingleOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["OrganisationId"] = new SelectList(_context.Set<Organisation>(), "Id", "Id", customer.OrganisationId);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,CustomerName,Email,Phone,Address,City,ZipCode,State,Country,AccountNumber,BankName,AccountName,SortCode,Website,Mobile,Note,Status,OrganisationId,DateCreated,DateUpdated,DateModified,IsDeleted")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrganisationId"] = new SelectList(_context.Set<Organisation>(), "Id", "Id", customer.OrganisationId);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.Organisation)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var customer = await _context.Customers.SingleOrDefaultAsync(m => m.Id == id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(Guid id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
        

          // the customer Views
        //public IActionResult CustomerStatement()
        //{
        //  return View();
        //}

    public IActionResult CustomerStatement(Guid CustomerId, DateTime StartDate, DateTime EndDate )
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


      if (CustomerId == Guid.Empty)
      {
        var customer = _context.Customers.Where(x => x.OrganisationId == org.Id);
        ViewData["CustomerId"] = new SelectList(_context.Set<Customer>().Where(x => x.OrganisationId == org.Id), "Id", "CustomerName", customer);

        CustomerStatementViewModel csVM = new CustomerStatementViewModel();

        return View(csVM);

      }
      else
      {

        var customer = _context.Customers.Where(x => x.OrganisationId == org.Id);
        ViewData["CustomerId"] = new SelectList(_context.Set<Customer>().Where(x => x.OrganisationId == org.Id), "Id", "CustomerName", customer);


        CustomerStatementViewModel csVM = new CustomerStatementViewModel();

        csVM.Customer = _context.Customers.Where(x => x.Id == CustomerId).FirstOrDefault();
        csVM.StartDate = StartDate;
        csVM.EndDate = EndDate;
        csVM.Organisation = org;

        List<CustomerState> lstCS = new List<CustomerState>();
        CustomerState onstate;
        var records = _context.Estimates.Where(x => x.CustomerId == CustomerId).Where(x => x.DateCreated >= StartDate && x.DateCreated <= EndDate).OrderBy(x => x.DateCreated).ToList();
        foreach (var item in records)
        {
          onstate = new CustomerState();

          if (item.PaymentStatus == "PAID")
          {
            onstate.Details = "Invoice";
            onstate.Amount = item.Total;
            onstate.StateDate = item.DateCreated;
            lstCS.Add(onstate);

            onstate = new CustomerState();
            onstate.Details = "Payment";
            onstate.Amount = item.Total;
            onstate.StateDate = item.DateCreated;
            lstCS.Add(onstate);


          }
          else if (item.Status == "INVOICE")
          {
            onstate.Details = "Invoice";
            onstate.Amount = item.Total;
            onstate.StateDate = item.DateCreated;
            lstCS.Add(onstate);


          }

        }

        csVM.CustomerStates = lstCS;


        return View(csVM);





      }


    }



  }
}
