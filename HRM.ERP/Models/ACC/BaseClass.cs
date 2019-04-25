using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HRM.ERP.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRM.ERP.Models.ACC
{
    public class BaseClass 
    {

        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateModified { get; set; }
        public bool IsDeleted { get; set; }

        public BaseClass()
        {
            DateCreated = DateTime.Now;
            DateUpdated = DateTime.Now;
            DateModified = DateTime.Now;
        }
    }
}
