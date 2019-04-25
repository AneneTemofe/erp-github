using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRM.ERP.Data;
using HRM.ERP.Models.ACC;

namespace HRM.ERP.Controllers
{
    public class RawEstimatesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RawEstimatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RawEstimates
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Estimates.Include(e => e.Customer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RawEstimates/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estimate = await _context.Estimates
                .Include(e => e.Customer)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (estimate == null)
            {
                return NotFound();
            }

            return View(estimate);
        }

        // GET: RawEstimates/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id");
            return View();
        }

        // POST: RawEstimates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerId,EstimateTitle,EstimateId,EstimateNo,EstimateDate,ExpireDate,POSO,Subheading,Footer,Memo,Total,GrandTotal,DateCreated,DateUpdated,DateModified,IsDeleted")] Estimate estimate)
        {
            if (ModelState.IsValid)
            {
                estimate.Id = Guid.NewGuid();
                _context.Add(estimate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", estimate.CustomerId);
            return View(estimate);
        }

        // GET: RawEstimates/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estimate = await _context.Estimates.SingleOrDefaultAsync(m => m.Id == id);
            if (estimate == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", estimate.CustomerId);
            return View(estimate);
        }

        // POST: RawEstimates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,CustomerId,EstimateTitle,EstimateId,EstimateNo,EstimateDate,ExpireDate,POSO,Subheading,Footer,Memo,Total,GrandTotal,DateCreated,DateUpdated,DateModified,IsDeleted")] Estimate estimate)
        {
            if (id != estimate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estimate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstimateExists(estimate.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", estimate.CustomerId);
            return View(estimate);
        }

        // GET: RawEstimates/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estimate = await _context.Estimates
                .Include(e => e.Customer)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (estimate == null)
            {
                return NotFound();
            }

            return View(estimate);
        }

        // POST: RawEstimates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var estimate = await _context.Estimates.SingleOrDefaultAsync(m => m.Id == id);
            _context.Estimates.Remove(estimate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstimateExists(Guid id)
        {
            return _context.Estimates.Any(e => e.Id == id);
        }
    }
}
