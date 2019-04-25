using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRM.ERP.Data;
using HRM.ERP.Models.HRM;
using System.Security.Claims;
using HRM.ERP.Models;

namespace HRM.ERP.Controllers
{
    public class PayGradesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PayGradesController(ApplicationDbContext context)
        {
            _context = context;
        }

    // GET: PayGrades
    [Route("HRM/Paygrade")]
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

      var applicationDbContext = _context.PayGrade.Where(x => x.OrganisationId == org.Id).Include(p => p.Job.JobTitle);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PayGrades/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payGrade = await _context.PayGrade
                .Include(p => p.Job)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (payGrade == null)
            {
                return NotFound();
            }

            return View(payGrade);
        }

    // GET: PayGrades/Create
    [Route("HRM/Paygrade/AddPayGrade")]
    public IActionResult Create(Guid? Id)
        {

            ViewData["JobId"] = new SelectList(_context.Job.Where(x => x.Id == Id).Include(p => p.JobTitle), "Id", "JobTitle.TitleName");
            var jobTitle = _context.Job.Where(m => m.Id == Id).FirstOrDefault();
            ViewBag.JobTitle = _context.JobTitle.Where(m => m.Id == jobTitle.JobTitleId).FirstOrDefault().TitleName;
            return View();
        }

        // POST: PayGrades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
    [Route("HRM/Paygrade/AddPayGrade")]
    public async Task<IActionResult> Create([Bind("Id,JobId,GradeName,Price,DateCreated,DateUpdated,DateModified,IsDeleted")] PayGrade payGrade)
        {
            if (ModelState.IsValid)
            {
        //var JobId = _context.Job.Where(x => x.JobTitle.Id == payGrade.JobId).FirstOrDefault().Id;

        //  payGrade.JobId = JobId;
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

        payGrade.Id = Guid.NewGuid();
                _context.Add(payGrade);
                await _context.SaveChangesAsync();
                return Redirect("/Jobs"); //RedirectToAction(nameof(Index));
            }
            ViewData["JobId"] = new SelectList(_context.Job, "Id", "Id", payGrade.JobId);
            return View(payGrade);
        }

        // GET: PayGrades/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payGrade = await _context.PayGrade.SingleOrDefaultAsync(m => m.Id == id);
            if (payGrade == null)
            {
                return NotFound();
            }
            ViewData["JobId"] = new SelectList(_context.Job, "Id", "Id", payGrade.JobId);
            return View(payGrade);
        }

        // POST: PayGrades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,JobId,GradeName,Price,DateCreated,DateUpdated,DateModified,IsDeleted")] PayGrade payGrade)
        {
            if (id != payGrade.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payGrade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PayGradeExists(payGrade.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("/Jobs"); //RedirectToAction(nameof(Index));
            }
            ViewData["JobId"] = new SelectList(_context.Job, "Id", "Id", payGrade.JobId);
            return View(payGrade);
        }

        // GET: PayGrades/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payGrade = await _context.PayGrade
                .Include(p => p.Job)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (payGrade == null)
            {
                return NotFound();
            }

            return View(payGrade);
        }

        // POST: PayGrades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var payGrade = await _context.PayGrade.SingleOrDefaultAsync(m => m.Id == id);
            _context.PayGrade.Remove(payGrade);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PayGradeExists(Guid id)
        {
            return _context.PayGrade.Any(e => e.Id == id);
        }
    }
}
