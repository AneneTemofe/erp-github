using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using HRM.ERP.Data;
using HRM.ERP.Models;
using HRM.ERP.Models.AccountViewModels;
using HRM.ERP.Models.HRM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRM.ERP.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext dbcontext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UsersController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            dbcontext = context;
        }
        [Route("Users")]
        public async Task<IActionResult> Index()
        {
      var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
      Organisation org;
      var otheruser = dbcontext.OtherUsers.Where(x => x.UserId == userId).FirstOrDefault();
      if (otheruser == null)
      {
        org = dbcontext.Organisation.Where(m => m.ApplicationUserId == userId).FirstOrDefault();

      }
      else
      {
        org = dbcontext.Organisation.Where(m => m.ApplicationUserId == otheruser.HostId).FirstOrDefault();

      }

      if (org == null)
      {
        ViewBag.IsOrg = "NotRegistered";
      }
      else
      {
        ViewBag.IsOrg = "Registered";

      }

      var orgUser = await dbcontext.OtherUsers.Where(x => x.OrganisationId == org.Id).ToListAsync();
      List<ApplicationUser> orgAppUser = new List<ApplicationUser>();
      ApplicationUser singleUser;
      foreach (var item in orgUser)
      {
        singleUser = new ApplicationUser();
        singleUser = dbcontext.Users.Where(c => c.Id == item.UserId.ToString()).FirstOrDefault();
        orgAppUser.Add(singleUser);
      }
            return View(orgAppUser);
        }

   

        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(RegisterViewModel model)
        {
      var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
      Organisation org = dbcontext.Organisation.Where(m => m.ApplicationUserId == userId).FirstOrDefault();

      if (ModelState.IsValid)
            {

                var user = new ApplicationUser()
                {
                    UserRole = model.UserRole,
                    EmployeeName = model.EmployeeName,
                    Usernames = model.Usernames,
                    Status = model.Status,
                    Email = model.Email,
                    UserName = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    
                };

                var results = await _userManager.CreateAsync(user, model.Password);

                if (results.Succeeded)
                {
                   
                    await _userManager.AddToRoleAsync(user, user.UserRole);

          OtherUser otherUser = new OtherUser()
          {
            Id = Guid.NewGuid(),
            OrganisationId = org.Id,
            UserId = Guid.Parse(user.Id),
            HostId = org.ApplicationUserId
          };

          dbcontext.Add(otherUser);
          dbcontext.SaveChanges();
                    //Send Mail After Employee Creation
                    //SmtpClient client = new SmtpClient("smtp.office365.com"); //set client 
                    //client.Port = 587;
                    //client.EnableSsl = true;
                    //client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    //client.UseDefaultCredentials = false;
                    //client.Credentials = new NetworkCredential("Wragbydev@wragbysolutions.com", "@Devops19"); //Mailing credential
                    ////mail body
                    //MailMessage mailMessage = new MailMessage();
                    //mailMessage.From = new MailAddress("Wragbydev@wragbysolutions.com");
                    //mailMessage.To.Add(model.Email); //Trainee mail here
                    //mailMessage.Body = "Hello " + model.EmployeeName + " You have just been onboarded on ERP4SME, Your email is " + model.Email + ". Please confirm your account by clicking this link: erp.com.";
                    //mailMessage.Subject = "MISTDO Account Created";
                    //client.Send(mailMessage);

                    return RedirectToAction(nameof(Index));
                }

            }
            return View();

        }
     
        [HttpGet]
        public async Task<IActionResult> EditUser(string Id)
        {
            var contract = await dbcontext.Users.SingleOrDefaultAsync(m => m.Id == Id);
            if (contract == null)
            {
                return NotFound();
            }
            return View(contract);
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(string Id, ApplicationUser model)
        {

            if (Id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //var user = new ApplicationUser()
                    //{
                    //    Id = model.Id,
                    //    UserRole = model.UserRole,
                    //    EmployeeName = model.EmployeeName,
                    //    Usernames = model.Usernames,
                    //    Status = model.Status,
                    //    Email = model.Email,
                    //    UserName = model.Email,
                    //    PhoneNumber = model.PhoneNumber,
                    //};

                    var AppUser = await _userManager.FindByIdAsync(Id);

                    AppUser.PhoneNumber = model.PhoneNumber;
                    AppUser.EmployeeName = model.EmployeeName;
                    AppUser.Email = model.Email;
                    AppUser.Usernames = model.Usernames;
                    AppUser.UserRole = model.UserRole;
                    AppUser.Status = model.Status;
                    AppUser.Id = model.Id;
                    var idResult = await _userManager.UpdateAsync(AppUser);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(model.Id))
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
            return View(model);
        }
        private bool UserExists(string Id)
        {
            return dbcontext.Users.Any(e => e.Id == Id);
        }
        public IActionResult DeleteUser()
        {
            return View();
        }

    }
}