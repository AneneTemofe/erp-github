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
    public class TestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TestController(ApplicationDbContext context)
        {
            _context = context;
        }

    //public ActionResult Index()
    //{
    //  CustomersEntities entities = new CustomersEntities();
    //  return View(entities.Customers);
    //}

    //public JsonResult InsertCustomers(List<Customer> customers)
    //{
    //  using (CustomersEntities entities = new CustomersEntities())
    //  {
    //    //Truncate Table to delete all old records.
    //    entities.Database.ExecuteSqlCommand("TRUNCATE TABLE [Customers]");

    //    //Check for NULL.
    //    if (customers == null)
    //    {
    //      customers = new List<Customer>();
    //    }

    //    //Loop and insert records.
    //    foreach (Customer customer in customers)
    //    {
    //      entities.Customers.Add(customer);
    //    }
    //    int insertedRecords = entities.SaveChanges();
    //    return Json(insertedRecords);
    //  }
    }
  }