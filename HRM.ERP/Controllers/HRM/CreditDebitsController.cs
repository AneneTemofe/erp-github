using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRM.ERP.Data;
using HRM.ERP.Models.HRM;

namespace HRM.ERP.Controllers.HRM
{
    public class CreditDebitsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CreditDebitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CreditDebits
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CreditDebits.Include(c => c.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CreditDebits/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creditDebit = await _context.CreditDebits
                .Include(c => c.Employee)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (creditDebit == null)
            {
                return NotFound();
            }

            return View(creditDebit);
        }

        // GET: CreditDebits/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Id");
            return View();
        }

        // POST: CreditDebits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,OrganisationId,CreditDebitType,Description,Amount,DateCreated,DateUpdated,DateModified,IsDeleted")] CreditDebit creditDebit)
        {
            if (ModelState.IsValid)
            {
                creditDebit.Id = Guid.NewGuid();
                _context.Add(creditDebit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Id", creditDebit.EmployeeId);
            return View(creditDebit);
        }

        // GET: CreditDebits/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creditDebit = await _context.CreditDebits.SingleOrDefaultAsync(m => m.Id == id);
            if (creditDebit == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Id", creditDebit.EmployeeId);
            return View(creditDebit);
        }

        // POST: CreditDebits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,EmployeeId,OrganisationId,CreditDebitType,Description,Amount,DateCreated,DateUpdated,DateModified,IsDeleted")] CreditDebit creditDebit)
        {
            if (id != creditDebit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(creditDebit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CreditDebitExists(creditDebit.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Id", creditDebit.EmployeeId);
            return View(creditDebit);
        }

        // GET: CreditDebits/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creditDebit = await _context.CreditDebits
                .Include(c => c.Employee)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (creditDebit == null)
            {
                return NotFound();
            }

            return View(creditDebit);
        }

        // POST: CreditDebits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var creditDebit = await _context.CreditDebits.SingleOrDefaultAsync(m => m.Id == id);
            _context.CreditDebits.Remove(creditDebit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CreditDebitExists(Guid id)
        {
            return _context.CreditDebits.Any(e => e.Id == id);
        }
    }
}
