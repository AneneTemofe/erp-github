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
    public class LeavesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeavesController(ApplicationDbContext context)
        {
            _context = context;
        }

    // GET: Leaves
    [Route("/HRM/PIM/Leave")]
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

      return View(await _context.Leave.Where(x => x.Employee.OrganisationId == org.Id).ToListAsync());
        }

    // GET: Leaves/Details/5
    [Route("/HRM/PIM/Leave/Details")]
    public async Task<IActionResult> Details(Guid? id)
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

      if (id == null)
            {
                return NotFound();
            }

            var leave = await _context.Leave.Where(x => x.Employee.OrganisationId == org.Id)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (leave == null)
            {
                return NotFound();
            }

            return View(leave);
        }

    // GET: Leaves/Create
    [Route("/HRM/PIM/Leave/Create")]
    public IActionResult Create()
        {
            return View();
        }

        // POST: Leaves/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
    [Route("/HRM/PIM/Leave")]
    public async Task<IActionResult> Create([Bind("Id,EmployeeId,LeaveTitle,Description,LeaveStartDate,LeaveEndDate,LeaveApprovedDate,IsApproved,Status,DateCreated,DateUpdated,DateModified,IsDeleted")] Leave leave)
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
                leave.Id = Guid.NewGuid();
                //leave.ApplicationUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                _context.Add(leave);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(leave);
        }

        // GET: Leaves/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leave = await _context.Leave.SingleOrDefaultAsync(m => m.Id == id);
            if (leave == null)
            {
                return NotFound();
            }
            return View(leave);
        }

        // POST: Leaves/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,EmployeeId,LeaveTitle,Description,LeaveStartDate,LeaveEndDate,LeaveApprovedDate,IsApproved,Status,DateCreated,DateUpdated,DateModified,IsDeleted")] Leave leave)
        {
            if (id != leave.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leave);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveExists(leave.Id))
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
            return View(leave);
        }

        // GET: Leaves/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leave = await _context.Leave
                .SingleOrDefaultAsync(m => m.Id == id);
            if (leave == null)
            {
                return NotFound();
            }

            return View(leave);
        }

        // POST: Leaves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var leave = await _context.Leave.SingleOrDefaultAsync(m => m.Id == id);
            _context.Leave.Remove(leave);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveExists(Guid id)
        {
            return _context.Leave.Any(e => e.Id == id);
        }
    }
}
