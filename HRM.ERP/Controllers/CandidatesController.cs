using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRM.ERP.Data;
using HRM.ERP.Models.HRM;
using HRM.ERP.Models;
using System.Security.Claims;

namespace HRM.ERP.Controllers
{
    public class CandidatesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CandidatesController(ApplicationDbContext context)
        {
            _context = context;
        }

    // GET: Candidates
    [Route("/HRM/Recuitment/Candidate")]
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

      var applicationDbContext = _context.Candidates.Where(x => x.Vacancy.JobTitle.OrganisationId == org.Id).Include(c => c.Vacancy).Include(c => c.Vacancy.JobTitle);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Candidates/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidates = await _context.Candidates
                .Include(c => c.Vacancy)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (candidates == null)
            {
                return NotFound();
            }

            return View(candidates);
        }

    // GET: Candidates/Create
    [Route("/HRM/Recuitment/Candidate/Apply")]
    public IActionResult Apply(Guid? Id)
        {
            var vac = _context.Vacancy.Where(c => c.Id == Id).FirstOrDefault();
            ViewBag.JobTitle= _context.JobTitle.Where(c => c.Id == vac.JobTitleId).FirstOrDefault().TitleName;
            ViewBag.VacancyId = Id;

            // ViewBag.VacancyTitle = _context.Vacancy.Where(x => x.Id == Id).FirstOrDefault().JobTitle.TitleName;

            return View();
        }

        // POST: Candidates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
    [Route("/HRM/Recuitment/Candidate/Apply")]
    public async Task<IActionResult> Apply([Bind("Id,VacancyId,FirstName,LastName,MiddleName,PhoneNumber,Email,JobVacancy,Resume,DateOfApplication,JoinDate,Comments,HiringManager,Status,DateCreated,DateUpdated,DateModified,IsDeleted")] Candidates candidates)
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

      if (ModelState.IsValid)
            {
                candidates.Id = Guid.NewGuid();
                _context.Add(candidates);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VacancyId"] = new SelectList(_context.Vacancy, "Id", "Id", candidates.VacancyId);
            return View(candidates);
        }

        // GET: Candidates/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidates = await _context.Candidates.SingleOrDefaultAsync(m => m.Id == id);
            if (candidates == null)
            {
                return NotFound();
            }
            ViewData["VacancyId"] = new SelectList(_context.Vacancy, "Id", "Id", candidates.VacancyId);
            return View(candidates);
        }

        // POST: Candidates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,VacancyId,FirstName,LastName,MiddleName,PhoneNumber,Email,JobVacancy,Resume,DateOfApplication,JoinDate,Comments,HiringManager,Status,DateCreated,DateUpdated,DateModified,IsDeleted")] Candidates candidates)
        {
            if (id != candidates.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(candidates);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CandidatesExists(candidates.Id))
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
            ViewData["VacancyId"] = new SelectList(_context.Vacancy, "Id", "Id", candidates.VacancyId);
            return View(candidates);
        }

        // GET: Candidates/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidates = await _context.Candidates
                .Include(c => c.Vacancy)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (candidates == null)
            {
                return NotFound();
            }

            return View(candidates);
        }

        // POST: Candidates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var candidates = await _context.Candidates.SingleOrDefaultAsync(m => m.Id == id);
            _context.Candidates.Remove(candidates);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CandidatesExists(Guid id)
        {
            return _context.Candidates.Any(e => e.Id == id);
        }
    }
}
