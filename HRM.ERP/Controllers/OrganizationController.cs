using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRM.ERP.Data;
using HRM.ERP.Models;
using HRM.ERP.Models.HRM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRM.ERP.Controllers
{
    public class OrganizationController : Controller
    {
        private readonly ApplicationDbContext dbcontext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public OrganizationController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            dbcontext = context;
        }

        [HttpGet]
        public async Task<IActionResult> EditBranch(Guid Id)
        {
            var linkView = await dbcontext.Organisations.SingleOrDefaultAsync(m => m.Id == Id);
            if (linkView == null)
            {
                return NotFound();
            }
            return View(linkView);

        }
        [HttpPost]
        public async Task<IActionResult> EditBranch(Guid id, Organisation model)
        {

            if (id != model.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var comp = new Organisation()
                    {
                        Id = model.Id,
                        OrganisationName = model.OrganisationName,
                        NoOfEmployees = model.NoOfEmployees,
                        PhoneNumber = model.PhoneNumber,
                        Address = model.Address,
                        State = model.State,
                        TaxId = model.TaxId,
                        RegistrationNo = model.RegistrationNo,
                        Email = model.Email,
                        City = model.City,
                        Country = model.Country,


                    };
                    dbcontext.Update(comp);
                    await dbcontext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrganisationsExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Branches));
            }
            return View(model);
        }
        private bool OrganisationsExists(Guid id)
        {
            return true;// dbcontext.Organisations.Any(e => e.id == id);
        }

        
        [HttpGet]
        public IActionResult AddBranch()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddBranch(Organisation model)
        {
            if (ModelState.IsValid)
            {

                var organise = new Organisation()
                {
                    Id = model.Id,
                    OrganisationName = model.OrganisationName,
                    NoOfEmployees = model.NoOfEmployees,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address,
                    State = model.State,
                    TaxId = model.TaxId,
                    RegistrationNo = model.RegistrationNo,
                    Email = model.Email,
                    City = model.City,
                    Country = model.Country,
                };

                dbcontext.Add(organise);
                await dbcontext.SaveChangesAsync();

                return RedirectToAction(nameof(Branches));
            }
            return View();
        }

        public async Task<IActionResult> Branches()
        {
            return View(await dbcontext.Organisations.ToListAsync());

        }

        private bool BranchExists(Guid? id)
        {
            return dbcontext.Organisations.Any(e => e.Id == id);

        }

        [HttpGet]
        public async Task<IActionResult> DeleteBranch(Guid id)
        {
            var brnch = await dbcontext.Organisations.SingleOrDefaultAsync(e => e.Id == id);
            dbcontext.Remove(brnch);
            await dbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Branches));

        }

        [HttpGet]
        public IActionResult CreateOrganization()
        {
            return View();
        }

        public async Task<IActionResult> General()
        {
            //return View(await dbcontext.Organisations.ToListAsync());
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> CreateOrganization(Organisation model)
        {
            if (ModelState.IsValid)
            {

                var organisation = new Organisation()
                {
                    Id = model.Id,
                    OrganisationName = model.OrganisationName,
                    NoOfEmployees = model.NoOfEmployees,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address,
                    State = model.State,
                    TaxId = model.TaxId,
                    RegistrationNo = model.RegistrationNo,
                    Email = model.Email,
                    City = model.City,
                    Country = model.Country,

                };

                dbcontext.Add(organisation);
                await dbcontext.SaveChangesAsync();

                return RedirectToAction(nameof(General));
            }
            return View();
        }

       
        [HttpGet]
        public async Task<IActionResult> EditGeneral(int id)
        {
            //var contract = await dbcontext.Organisations.SingleOrDefaultAsync(m => m.id == id);
            //if(contract == null)
            //{
            //    return NotFound();
            //}
            //return View(contract);
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> EditGeneral(Guid id, Organisation model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var company = new Organisation()
                    {
                        Id = model.Id,
                        OrganisationName = model.OrganisationName,
                        NoOfEmployees = model.NoOfEmployees,
                        PhoneNumber = model.PhoneNumber,
                        Address = model.Address,
                        State = model.State,
                        TaxId = model.TaxId,
                        RegistrationNo = model.RegistrationNo,
                        Email = model.Email,
                        City = model.City,
                        Country = model.Country,

                    };
                    dbcontext.Update(company);
                    await dbcontext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrganisationExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(General));
            }
            return View(model);
        }
        private bool OrganisationExists(Guid id)
        {
            return true; // dbcontext.Organisations.Any(e => e.id == id);
        }

        //public IActionResult DeleteOrganisation()
        //{
        //    return View();
        //}

        private bool OrganisationExists(Guid? id)
        {
            return true; // dbcontext.Organisations.Any(e => e.id == id);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteOrganisation(Guid id)
        {
            //var gene = await dbcontext.Organisations.SingleOrDefaultAsync(e => e.id == id);
            //dbcontext.Remove(gene);
            //await dbcontext.SaveChangesAsync();

            //return RedirectToAction(nameof(General));
            return View();

        }


        public IActionResult Structure()
        {
            return View();
        }

    }
}