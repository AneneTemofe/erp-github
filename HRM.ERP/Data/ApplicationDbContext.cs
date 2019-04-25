using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HRM.ERP.Models;
using HRM.ERP.Models.HRM;
using HRM.ERP.Models.ACC;

namespace HRM.ERP.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<JobTitle> JobTitle { get; set; }
        public DbSet<Job> Job { get; set; }
        public DbSet<PayGrade> PayGrade { get; set; }
        public DbSet<Vacancy> Vacancy { get; set; }
        public DbSet<Candidates> Candidates { get; set; }
        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Education> Education { get; set; }
        public DbSet<Skill> Skill { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<Leave> Leave { get; set; }
        public DbSet<Employee> Employee { get; set; }

        public DbSet<Models.Organisation> Organisation { get; set; }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<ProductService> ProductServices { get; set; }
        public DbSet<ItemsEIR> ItemsEIRs { get; set; }
        public DbSet<Estimate> Estimates { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<ContractType> ContractTypes { get; set; }

    public DbSet<OtherUser> OtherUsers { get; set; }
    public DbSet<Payroll> Payrolls { get; set; }
    public DbSet<CreditDebit> CreditDebits { get; set; }
    public DbSet<HRM.ERP.Models.ACC.Vendor> Vendor { get; set; }
    public DbSet<Receipt> REceipts { get; set; }
    public DbSet<FileUploads> FileUpload { get; set; }
    public DbSet<HRM.ERP.Models.ACC.Bill> Bill { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    //      public DbSet<Employee> Employee { get; set; }

    //    public DbSet<Dependants> Dependants { get; set; }

    //  public DbSet<EmergencyContact> EmergencyContact { get; set; }

    //public DbSet<EmploymentStatus> EmploymentStatus { get; set; }

    //public DbSet<PayGrade> PayGrade { get; set; }


    //      public DbSet<Skills> Skills { get; set; }

    //      public DbSet<Language> Language { get; set; }

    //       public DbSet<Supervisor> Supervisor { get; set; }

    //       public DbSet<SubOrdinates> SubOrdinates { get; set; }

    //       public DbSet<Salary> Salary { get; set; }



    //      public DbSet<JobApplicants> JobApplicants { get; set; }

    //      public DbSet<Performance> Performance { get; set; }



  }
}
