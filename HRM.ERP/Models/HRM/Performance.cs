using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.ERP.Models.HRM
{
    public class Performance
    {
        public int id { get; set; }

        public string jobTitle { get; set; }

        public string keyPerformanceIndicator { get; set; }

        public int minimumRating { get; set; }

        public int maximumRating { get; set; }

        public int refaultScale { get; set; }

        public DateTime dateCreated { get; set; }

    }
}
