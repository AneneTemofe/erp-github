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
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

    // GET: Employees
    [Route("/HRM/PIM/Employee")]
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

      var applicationDbContext = _context.Employee.Where(x => x.OrganisationId == org.Id).Include(e => e.Job.JobTitle);
            return View(await applicationDbContext.ToListAsync());
        }
    [Route("/HRM/PIM/Employee/Report")]
    public async Task<IActionResult> Reports()
        {
            return View(await _context.Employee.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.Job)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

    // GET: Employees/Create
    [Route("/HRM/PIM/Employee/Create")]
    public IActionResult Create()
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

      ViewData["JobId"] = new SelectList(_context.Job.Where(x => x.OrganisationId == org.Id).Include(e => e.JobTitle).Include(c => c.Department), "Id", "JobTitle.TitleName");
      return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
    [Route("/HRM/PIM/Employee/Create")]
    public async Task<IActionResult> Create([Bind("Id,ApplicationUserId,JobId,FirstName,LastName,MiddleName,EmployeeId,Photograph,Status,DOB,Nationality,Gender,MaritalStatus,Address,MobileNo,WorkTelephone,Email,AccountNo,AccountType,SortCode,contractType,JoinDate,DateCreated,DateUpdated,DateModified,IsDeleted")] Models.HRM.Employee employee)
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
                employee.Id = Guid.NewGuid();
        employee.OrganisationId = org.Id;
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JobId"] = new SelectList(_context.Job, "Id", "Id", employee.JobId);
            return View(employee);
        }

        //public async Task<IActionResult> Create([Bind("Id,ApplicationUserId,FirstName,LastName,MiddleName,EmployeeId,Photograph,Status,DOB,Nationality,Gender,MaritalStatus,Address,MobileNo,WorkTelephone,Email,AccountNo,AccountType,SortCode,contractType,JoinDate,DateCreated,DateUpdated,DateModified,IsDeleted")] Employee employee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        employee.Id = Guid.NewGuid();
        //        _context.Add(employee);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(employee);
        //}

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.SingleOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["JobId"] = new SelectList(_context.Job, "Id", "Id", employee.JobId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ApplicationUserId,JobId,FirstName,LastName,MiddleName,EmployeeId,Photograph,Status,DOB,Nationality,Gender,MaritalStatus,Address,MobileNo,WorkTelephone,Email,AccountNo,AccountType,SortCode,contractType,JoinDate,DateCreated,DateUpdated,DateModified,IsDeleted")] Models.HRM.Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
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
            ViewData["JobId"] = new SelectList(_context.Job, "Id", "Id", employee.JobId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .SingleOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var employee = await _context.Employee.SingleOrDefaultAsync(m => m.Id == id);
            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(Guid id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }
    }
}
