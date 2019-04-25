using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.ERP.Models.HRM
{
    public class Vacancy : BaseClass
    {
        public Guid Id { get; set; }
        public Guid JobTitleId { get; set; }
    public Guid OrganisationId { get; set; }
    public string VacancyName { get; set; }

        public string HiringManager { get; set; }

        public int NoOfPositions { get; set; }

        public string Description { get; set; }
        public virtual JobTitle JobTitle { get; set; }

    }
}
