using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRM.ERP.Data;
using HRM.ERP.Models.ACC;
using HRM.ERP.Models;
using System.Security.Claims;

namespace HRM.ERP.Controllers.Accounting
{
    public class ProductServicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductServices
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProductServices.Include(p => p.Organisation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProductServices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productService = await _context.ProductServices
                .Include(p => p.Organisation)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (productService == null)
            {
                return NotFound();
            }

            return View(productService);
        }

        // GET: ProductServices/Create
        public IActionResult Create()
        {
            ViewData["OrganisationId"] = new SelectList(_context.Set<Organisation>(), "Id", "Id");
            return View();
        }

        // POST: ProductServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,SalesTax,Sell,Status,OrganisationId,DateCreated,DateUpdated,DateModified,IsDeleted")] ProductService productService)
        {
            var Id = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var orgId = _context.Organisation.Where(x => x.ApplicationUserId == Id).FirstOrDefault().Id;

            productService.OrganisationId = orgId;

            if (ModelState.IsValid)
            {
                productService.Id = Guid.NewGuid();
                _context.Add(productService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrganisationId"] = new SelectList(_context.Set<Organisation>(), "Id", "Id", productService.OrganisationId);
            return View(productService);
        }

        // GET: ProductServices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productService = await _context.ProductServices.SingleOrDefaultAsync(m => m.Id == id);
            if (productService == null)
            {
                return NotFound();
            }
            ViewData["OrganisationId"] = new SelectList(_context.Set<Organisation>(), "Id", "Id", productService.OrganisationId);
            return View(productService);
        }

        // POST: ProductServices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,Price,SalesTax,Sell,Status,OrganisationId,DateCreated,DateUpdated,DateModified,IsDeleted")] ProductService productService)
        {
            if (id != productService.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductServiceExists(productService.Id))
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
            ViewData["OrganisationId"] = new SelectList(_context.Set<Organisation>(), "Id", "Id", productService.OrganisationId);
            return View(productService);
        }

        // GET: ProductServices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productService = await _context.ProductServices
                .Include(p => p.Organisation)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (productService == null)
            {
                return NotFound();
            }

            return View(productService);
        }

        // POST: ProductServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var productService = await _context.ProductServices.SingleOrDefaultAsync(m => m.Id == id);
            _context.ProductServices.Remove(productService);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductServiceExists(Guid id)
        {
            return _context.ProductServices.Any(e => e.Id == id);
        }
    }
}
