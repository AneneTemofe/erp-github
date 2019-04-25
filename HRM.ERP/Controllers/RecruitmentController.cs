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
    public class RecruitmentController : Controller
    {
        private readonly ApplicationDbContext dbcontext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public RecruitmentController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            dbcontext = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddRecruitment()
        {
            return View();
        }
        public async Task<IActionResult> CandidatesIndex()
        {
            return View(await dbcontext.Candidates.ToListAsync());
        }
        [HttpGet]
        public IActionResult AddCandidates()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCandidates(Candidates model)
        {
            if (ModelState.IsValid)
            {
                var Candid = new Candidates()
                {
                    Id = model.Id,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MiddleName = model.MiddleName,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    JobVacancy = model.JobVacancy,
                    Resume = model.Resume,
                    DateOfApplication = model.DateOfApplication,
                    JoinDate = model.JoinDate,
                    Comments = model.Comments,
                    HiringManager = model.HiringManager,
                    Status = model.Status
                };

                dbcontext.Add(Candid);
                await dbcontext.SaveChangesAsync();

                return RedirectToAction(nameof(CandidatesIndex));

            }
            return View();
        }





    }
}