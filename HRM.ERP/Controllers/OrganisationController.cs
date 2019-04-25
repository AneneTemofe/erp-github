using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRM.ERP.Data;
using HRM.ERP.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace HRM.ERP.Controllers
{
    [Authorize]
    public class OrganisationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrganisationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Organisation
        public async Task<IActionResult> Index()
        {
            return View(await _context.Organisation.ToListAsync());
        }

        // GET: Organisation/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organisation = await _context.Organisation
                .SingleOrDefaultAsync(m => m.Id == id);
            if (organisation == null)
            {
                return NotFound();
            }

            return View(organisation);
        }


    public ActionResult MyOrganisation()
    {
      var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
      Organisation org;
      var otheruser = _context.OtherUsers.Where(x => x.UserId == userId).FirstOrDefault();
      if(otheruser == null)
      {
        org = _context.Organisation.Where(m => m.ApplicationUserId == userId).FirstOrDefault();

      }
      else
      {
        org = _context.Organisation.Where(m => m.ApplicationUserId == otheruser.HostId).FirstOrDefault();

      }

      if (org == null)
      {
        return RedirectToAction(nameof(Edit));
      }

      return View(org);
    }

    // GET: Organisation/Create

        // POST: Organisation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrganisationName,NoOfEmployees,PhoneNumber,Email,Address,City,ZipCode,TaxId,RegistrationNo,State,Country,DateCreated,DateUpdated,DateModified,IsDeleted")] Organisation organisation)
        {
            organisation.ApplicationUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (ModelState.IsValid)
            {
                organisation.Id = Guid.NewGuid();
                _context.Add(organisation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(organisation);
        }

        // GET: Organisation/Edit/5
        public async Task<IActionResult> Edit()
        {
      //if (id == null)
      //{
      //    return NotFound();
      //}
      var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

      var organisation = await _context.Organisation.SingleOrDefaultAsync(m => m.ApplicationUserId == userId);
            if (organisation == null)
            {
        Organisation organ = new Organisation();
        organ.ApplicationUserId = userId;
        organ.Id = Guid.NewGuid();

        _context.Add(organ);
        _context.SaveChanges();

                return View(organ);
            }
            return View(organisation);
        }

        // POST: Organisation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,OrganisationName,NoOfEmployees,PhoneNumber,Email,Address,City,ZipCode,TaxId,RegistrationNo,State,Country,DateCreated,DateUpdated,DateModified,IsDeleted")] Organisation organisation)
        {
            if (id != organisation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
          var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

          organisation.ApplicationUserId = userId;
                    _context.Update(organisation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrganisationExists(organisation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(MyOrganisation));
            }
            return View(organisation);
        }

        // GET: Organisation/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organisation = await _context.Organisation
                .SingleOrDefaultAsync(m => m.Id == id);
            if (organisation == null)
            {
                return NotFound();
            }

            return View(organisation);
        }

        // POST: Organisation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var organisation = await _context.Organisation.SingleOrDefaultAsync(m => m.Id == id);
            _context.Organisation.Remove(organisation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrganisationExists(Guid id)
        {
            return _context.Organisation.Any(e => e.Id == id);
        }
    }
}
