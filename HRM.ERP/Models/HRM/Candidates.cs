using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.ERP.Models.HRM
{
    public class Candidates : BaseClass
    {
        public Guid Id { get; set; }

        public Guid VacancyId { get; set; }

        public string Title { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public int PhoneNumber { get; set; }

        public string Email { get; set; }

        public string DateOfBirth { get; set; }

        public string StateOfOrigin { get; set; }

        public string Institution { get; set; }

        public string CourseStudy { get; set; }

        public string QualificationDegree { get; set; }

        public string ClassOfDegree { get; set; }

        public string MasterInstitution { get; set; }

        public string MastersCourseOfStudy { get; set; }

        public string ProfessionalQualification { get; set; }

        public int YearsOfExperience { get; set; }

        public string PreviousCurrentEmployer { get; set; }

        public string Designation { get; set; }

        public int YearsOfExperienceInMostRecentDesignation { get; set; }

        public string Location { get; set; }

        public string WhichIndustry { get; set; }

        public int From { get; set; }

        public int To { get; set; }

        public string GeneralAreaOfSpecialization { get; set; }

        public string Employer { get; set; }

        public string Role { get; set; }

        public string JobVacancy { get; set; }

        public string Resume { get; set; }

        public DateTime DateOfApplication { get; set; }

        public DateTime JoinDate { get; set; }

        public string Comments { get; set; }

        public string HiringManager { get; set; }

        public string Status { get; set; }


        public virtual Vacancy Vacancy { get; set; }

    }
}
