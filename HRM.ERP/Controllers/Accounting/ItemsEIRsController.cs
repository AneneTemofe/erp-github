using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRM.ERP.Data;
using HRM.ERP.Models.ACC;

namespace HRM.ERP.Controllers.Accounting
{
    public class ItemsEIRsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemsEIRsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ItemsEIRs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ItemsEIRs.Include(i => i.Estimate);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ItemsEIRs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemsEIR = await _context.ItemsEIRs
                .Include(i => i.Estimate)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (itemsEIR == null)
            {
                return NotFound();
            }

            return View(itemsEIR);
        }

        // GET: ItemsEIRs/Create
        public IActionResult Create()
        {
            ViewData["EstimateId"] = new SelectList(_context.Estimates, "Id", "Id");
            return View();
        }

        // POST: ItemsEIRs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EstimateId,Item,Description,Quantity,Price,Discount,Amount,DateCreated,DateUpdated,DateModified,IsDeleted")] ItemsEIR itemsEIR)
        {
            if (ModelState.IsValid)
            {
                itemsEIR.Id = Guid.NewGuid();
                _context.Add(itemsEIR);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstimateId"] = new SelectList(_context.Estimates, "Id", "Id", itemsEIR.EstimateId);
            return View(itemsEIR);
        }

        // GET: ItemsEIRs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemsEIR = await _context.ItemsEIRs.SingleOrDefaultAsync(m => m.Id == id);
            if (itemsEIR == null)
            {
                return NotFound();
            }
            ViewData["EstimateId"] = new SelectList(_context.Estimates, "Id", "Id", itemsEIR.EstimateId);
            return View(itemsEIR);
        }

        // POST: ItemsEIRs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,EstimateId,Item,Description,Quantity,Price,Discount,Amount,DateCreated,DateUpdated,DateModified,IsDeleted")] ItemsEIR itemsEIR)
        {
            if (id != itemsEIR.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemsEIR);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemsEIRExists(itemsEIR.Id))
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
            ViewData["EstimateId"] = new SelectList(_context.Estimates, "Id", "Id", itemsEIR.EstimateId);
            return View(itemsEIR);
        }

        // GET: ItemsEIRs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemsEIR = await _context.ItemsEIRs
                .Include(i => i.Estimate)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (itemsEIR == null)
            {
                return NotFound();
            }

            return View(itemsEIR);
        }

        // POST: ItemsEIRs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var itemsEIR = await _context.ItemsEIRs.SingleOrDefaultAsync(m => m.Id == id);
            _context.ItemsEIRs.Remove(itemsEIR);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemsEIRExists(Guid id)
        {
            return _context.ItemsEIRs.Any(e => e.Id == id);
        }
    }
}
