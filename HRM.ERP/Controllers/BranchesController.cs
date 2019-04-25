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
    public class BranchesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BranchesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Branches
        [Route("HRM/Branch")]
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

      return View(await _context.Branches.Where(x => x.OrganisationId == org.Id).ToListAsync());
        }

        // GET: Branches/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _context.Branches
                .SingleOrDefaultAsync(m => m.Id == id);
            if (branch == null)
            {
                return NotFound();
            }

            return View(branch);
        }

        // GET: Branches/Create
        [Route("HRM/Branch/Addbranch")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Branches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
    [Route("HRM/Branch/Addbranch")]
    public async Task<IActionResult> Create([Bind("Id,BranchName,PhoneNumber,Email,Address,City,ZipCode,State,Country,Status,DateCreated,DateUpdated,DateModified,IsDeleted")] Branch branch)
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

        branch.Id = Guid.NewGuid();
        branch.OrganisationId = org.Id;
                _context.Add(branch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(branch);
        }

    // GET: Branches/Edit/5
    [Route("HRM/Branch/Editbranch")]
    public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _context.Branches.SingleOrDefaultAsync(m => m.Id == id);
            if (branch == null)
            {
                return NotFound();
            }
            return View(branch);
        }

        // POST: Branches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
    [Route("HRM/Branch/Editbranch")]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,BranchName,PhoneNumber,Email,Address,City,ZipCode,State,Country,Status,DateCreated,DateUpdated,DateModified,IsDeleted")] Branch branch)
        {
            if (id != branch.Id)
            {
                return NotFound();
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


      if (ModelState.IsValid)
            {
                try
                {
          branch.OrganisationId = org.Id;

          _context.Update(branch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BranchExists(branch.Id))
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
            return View(branch);
        }

    // GET: Branches/Delete/5
    [Route("HRM/Branch/Deletebranch")]
    public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _context.Branches
                .SingleOrDefaultAsync(m => m.Id == id);
            if (branch == null)
            {
                return NotFound();
            }

            return View(branch);
        }

        // POST: Branches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
    [Route("HRM/Branch/Deletebranch")]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var branch = await _context.Branches.SingleOrDefaultAsync(m => m.Id == id);
            _context.Branches.Remove(branch);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BranchExists(Guid id)
        {
            return _context.Branches.Any(e => e.Id == id);
        }
    }
}
