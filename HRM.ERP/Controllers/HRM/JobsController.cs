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
using Microsoft.AspNetCore.Authorization;

namespace HRM.ERP.Controllers.HRM
{
  [Authorize]
    public class JobsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobsController(ApplicationDbContext context)
        {
            _context = context;
        }

    // GET: Jobs
    [Route("HRM/Job")]
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

      var applicationDbContext = _context.Job.Include(j => j.ContractType).Include(j => j.JobTitle).Where(x => x.OrganisationId == org.Id);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Job
                .Include(j => j.ContractType)
                .Include(j => j.JobTitle)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

    // GET: Jobs/Create
    [Route("HRM/Job/AddJob")]
    public IActionResult Create()
        {
            ViewData["ContractTypeId"] = new SelectList(_context.ContractTypes, "Id", "Name");
            ViewData["JobTitleId"] = new SelectList(_context.JobTitle, "Id", "TitleName");
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "DepartmentName");

      return View();
        }

    // POST: Jobs/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [Route("HRM/Job/AddJob")]
    [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,JobTitleId,ContractTypeId,OrganisationId,DepartmentId,ContractStartDate,ContractEndDate,JobDescription,DateCreated,DateUpdated,DateModified,IsDeleted")] Job job)
        {
            if (ModelState.IsValid)
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
        job.OrganisationId = org.Id;
        job.Id = Guid.NewGuid();
                _context.Add(job);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContractTypeId"] = new SelectList(_context.ContractTypes, "Id", "Id", job.ContractTypeId);
            ViewData["JobTitleId"] = new SelectList(_context.JobTitle, "Id", "Id", job.JobTitleId);
            return View(job);
        }

        // GET: Jobs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Job.SingleOrDefaultAsync(m => m.Id == id);
            if (job == null)
            {
                return NotFound();
            }
            ViewData["ContractTypeId"] = new SelectList(_context.ContractTypes, "Id", "Id", job.ContractTypeId);
            ViewData["JobTitleId"] = new SelectList(_context.JobTitle, "Id", "Id", job.JobTitleId);
            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,JobTitleId,ContractTypeId,OrganisationId,ContractStartDate,ContractEndDate,JobDescription,DateCreated,DateUpdated,DateModified,IsDeleted")] Job job)
        {
            if (id != job.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(job);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobExists(job.Id))
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
            ViewData["ContractTypeId"] = new SelectList(_context.ContractTypes, "Id", "Id", job.ContractTypeId);
            ViewData["JobTitleId"] = new SelectList(_context.JobTitle, "Id", "Id", job.JobTitleId);
            return View(job);
        }

        // GET: Jobs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Job
                .Include(j => j.ContractType)
                .Include(j => j.JobTitle)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var job = await _context.Job.SingleOrDefaultAsync(m => m.Id == id);
            _context.Job.Remove(job);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobExists(Guid id)
        {
            return _context.Job.Any(e => e.Id == id);
        }
    }
}
