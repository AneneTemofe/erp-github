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
    public class ContractTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContractTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

    // GET: ContractTypes
    [Route("HRM/ContractType")]
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

      return View(await _context.ContractTypes.Where(x => x.OrganisationId == org.Id).ToListAsync());
        }

        // GET: ContractTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractType = await _context.ContractTypes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (contractType == null)
            {
                return NotFound();
            }

            return View(contractType);
        }

    // GET: ContractTypes/Create
    [Route("HRM/ContractType/AddContractType")]
    public IActionResult Create()
        {
            return View();
        }

    // POST: ContractTypes/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [Route("HRM/ContractType/AddContractType")]
    [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrganisationId,Name,Description,Status,DateCreated,DateUpdated,DateModified,IsDeleted")] ContractType contractType)
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

        contractType.OrganisationId = org.Id;
        contractType.Id = Guid.NewGuid();
                _context.Add(contractType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contractType);
        }

        // GET: ContractTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractType = await _context.ContractTypes.SingleOrDefaultAsync(m => m.Id == id);
            if (contractType == null)
            {
                return NotFound();
            }
            return View(contractType);
        }

        // POST: ContractTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,OrganisationId,Name,Description,Status,DateCreated,DateUpdated,DateModified,IsDeleted")] ContractType contractType)
        {
            if (id != contractType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contractType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractTypeExists(contractType.Id))
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
            return View(contractType);
        }

        // GET: ContractTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractType = await _context.ContractTypes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (contractType == null)
            {
                return NotFound();
            }

            return View(contractType);
        }

        // POST: ContractTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var contractType = await _context.ContractTypes.SingleOrDefaultAsync(m => m.Id == id);
            _context.ContractTypes.Remove(contractType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContractTypeExists(Guid id)
        {
            return _context.ContractTypes.Any(e => e.Id == id);
        }
    }
}
