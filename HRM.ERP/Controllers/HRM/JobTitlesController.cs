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

namespace HRM.ERP.Controllers.HRM
{
    public class JobTitlesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobTitlesController(ApplicationDbContext context)
        {
            _context = context;
        }

    // GET: JobTitles
    [Route("HRM/JobTitle")]
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

      return View(await _context.JobTitle.Where(x => x.OrganisationId == org.Id).ToListAsync());
        }

        // GET: JobTitles/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobTitle = await _context.JobTitle
                .SingleOrDefaultAsync(m => m.Id == id);
            if (jobTitle == null)
            {
                return NotFound();
            }

            return View(jobTitle);
        }

    // GET: JobTitles/Create
    [Route("HRM/JobTitle/AddJobTitle")]
    public IActionResult Create()
        {
            return View();
        }

    // POST: JobTitles/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [Route("HRM/JobTitle/AddJobTitle")]
    [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrganisationId,TitleName,Description,DateCreated,DateUpdated,DateModified,IsDeleted")] JobTitle jobTitle)
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

        jobTitle.OrganisationId = org.Id;
        jobTitle.Id = Guid.NewGuid();
                _context.Add(jobTitle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jobTitle);
        }

        // GET: JobTitles/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobTitle = await _context.JobTitle.SingleOrDefaultAsync(m => m.Id == id);
            if (jobTitle == null)
            {
                return NotFound();
            }
            return View(jobTitle);
        }

        // POST: JobTitles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,OrganisationId,TitleName,Description,DateCreated,DateUpdated,DateModified,IsDeleted")] JobTitle jobTitle)
        {
            if (id != jobTitle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobTitle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobTitleExists(jobTitle.Id))
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
            return View(jobTitle);
        }

        // GET: JobTitles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobTitle = await _context.JobTitle
                .SingleOrDefaultAsync(m => m.Id == id);
            if (jobTitle == null)
            {
                return NotFound();
            }

            return View(jobTitle);
        }

        // POST: JobTitles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var jobTitle = await _context.JobTitle.SingleOrDefaultAsync(m => m.Id == id);
            _context.JobTitle.Remove(jobTitle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobTitleExists(Guid id)
        {
            return _context.JobTitle.Any(e => e.Id == id);
        }
    }
}
