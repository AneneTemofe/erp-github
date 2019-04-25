using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.ERP.Models.HRM
{
    public class Language : BaseClass 
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string LanguageName { get; set; }
        public string Level { get; set; }

        public virtual Employee Employee { get; set; }

    }
}
