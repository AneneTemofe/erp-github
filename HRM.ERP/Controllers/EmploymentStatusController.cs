using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRM.ERP.Data;
using HRM.ERP.Models.HRM;

namespace HRM.ERP.Controllers
{
    public class EmploymentStatusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmploymentStatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EmploymentStatus
        public async Task<IActionResult> Index()
        {
            return View(/*await _context.EmploymentStatus.ToListAsync()*/);
        }

        // GET: EmploymentStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var employmentStatus = await _context.EmploymentStatus
            //    .SingleOrDefaultAsync(m => m.Id == id);
            //if (employmentStatus == null)
            //{
            //    return NotFound();
            //}

            return View(/*employmentStatus*/);
        }

        // GET: EmploymentStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmploymentStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StatusName,DateCreated")] EmploymentStatus employmentStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employmentStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employmentStatus);
        }

        // GET: EmploymentStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var employmentStatus = await _context.EmploymentStatus.SingleOrDefaultAsync(m => m.Id == id);
            //if (employmentStatus == null)
            //{
            //    return NotFound();
            //}
            //return View(employmentStatus);
            return View();

        }

        // POST: EmploymentStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StatusName,DateCreated")] EmploymentStatus employmentStatus)
        {
            if (id != employmentStatus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employmentStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmploymentStatusExists(employmentStatus.Id))
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
            return View(employmentStatus);
        }

        // GET: EmploymentStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var employmentStatus = await _context.EmploymentStatus
            //    .SingleOrDefaultAsync(m => m.Id == id);
            //if (employmentStatus == null)
            //{
            //    return NotFound();
            //}

            //return View(employmentStatus);
            return View();

        }

        // POST: EmploymentStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var employmentStatus = await _context.EmploymentStatus.SingleOrDefaultAsync(m => m.Id == id);
            //_context.EmploymentStatus.Remove(employmentStatus);
            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return View();

        }

        private bool EmploymentStatusExists(int id)
        {
            return true; // _context.EmploymentStatus.Any(e => e.Id == id);
        }
    }
}
