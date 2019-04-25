﻿// <auto-generated />
using HRM.ERP.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace HRM.ERP.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190401120405_pay_grade")]
    partial class pay_grade
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HRM.ERP.Models.ACC.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountName");

                    b.Property<string>("AccountNumber");

                    b.Property<string>("Address");

                    b.Property<string>("BankName");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("CustomerName");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("Email");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Mobile");

                    b.Property<string>("Note");

                    b.Property<Guid>("OrganisationId");

                    b.Property<string>("Phone");

                    b.Property<string>("SortCode");

                    b.Property<string>("State");

                    b.Property<string>("Status");

                    b.Property<string>("Website");

                    b.Property<string>("ZipCode");

                    b.HasKey("Id");

                    b.HasIndex("OrganisationId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("HRM.ERP.Models.ACC.Estimate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CustomerId");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<DateTime>("EstimateDate");

                    b.Property<string>("EstimateId");

                    b.Property<int>("EstimateNo");

                    b.Property<string>("EstimateTitle");

                    b.Property<DateTime>("ExpireDate");

                    b.Property<string>("Footer");

                    b.Property<float>("GrandTotal");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Memo");

                    b.Property<string>("POSO");

                    b.Property<string>("Status");

                    b.Property<string>("Subheading");

                    b.Property<float>("Total");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Estimates");
                });

            modelBuilder.Entity("HRM.ERP.Models.ACC.ItemsEIR", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("Amount");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("Description");

                    b.Property<float>("Discount");

                    b.Property<Guid>("EstimateId");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Item");

                    b.Property<float>("Price");

                    b.Property<float>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("EstimateId");

                    b.ToTable("ItemsEIRs");
                });

            modelBuilder.Entity("HRM.ERP.Models.ACC.ProductService", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<Guid>("OrganisationId");

                    b.Property<float>("Price");

                    b.Property<float>("SalesTax");

                    b.Property<bool>("Sell");

                    b.Property<string>("Status");

                    b.HasKey("Id");

                    b.HasIndex("OrganisationId");

                    b.ToTable("ProductServices");
                });

            modelBuilder.Entity("HRM.ERP.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("EmployeeName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Status");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<string>("UserRole");

                    b.Property<string>("Usernames");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("HRM.ERP.Models.HRM.Branch", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("BranchName");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("Email");

                    b.Property<bool>("IsDeleted");

                    b.Property<Guid>("OrganisationId");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("State");

                    b.Property<string>("Status");

                    b.Property<int>("ZipCode");

                    b.HasKey("Id");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("HRM.ERP.Models.HRM.Candidates", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClassOfDegree");

                    b.Property<string>("Comments");

                    b.Property<string>("CourseStudy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<DateTime>("DateOfApplication");

                    b.Property<string>("DateOfBirth");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("Designation");

                    b.Property<string>("Email");

                    b.Property<string>("Employer");

                    b.Property<string>("FirstName");

                    b.Property<int>("From");

                    b.Property<string>("GeneralAreaOfSpecialization");

                    b.Property<string>("HiringManager");

                    b.Property<string>("Institution");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("JobVacancy");

                    b.Property<DateTime>("JoinDate");

                    b.Property<string>("LastName");

                    b.Property<string>("Location");

                    b.Property<string>("MasterInstitution");

                    b.Property<string>("MastersCourseOfStudy");

                    b.Property<string>("MiddleName");

                    b.Property<int>("PhoneNumber");

                    b.Property<string>("PreviousCurrentEmployer");

                    b.Property<string>("ProfessionalQualification");

                    b.Property<string>("QualificationDegree");

                    b.Property<string>("Resume");

                    b.Property<string>("Role");

                    b.Property<string>("StateOfOrigin");

                    b.Property<string>("Status");

                    b.Property<string>("Title");

                    b.Property<int>("To");

                    b.Property<Guid>("VacancyId");

                    b.Property<string>("WhichIndustry");

                    b.Property<int>("YearsOfExperience");

                    b.Property<int>("YearsOfExperienceInMostRecentDesignation");

                    b.HasKey("Id");

                    b.HasIndex("VacancyId");

                    b.ToTable("Candidates");
                });

            modelBuilder.Entity("HRM.ERP.Models.HRM.ContractType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<Guid>("OrganisationId");

                    b.Property<string>("Status");

                    b.HasKey("Id");

                    b.ToTable("ContractTypes");
                });

            modelBuilder.Entity("HRM.ERP.Models.HRM.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("DepartmentName");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeleted");

                    b.Property<Guid>("OrganisationId");

                    b.Property<string>("Status");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("HRM.ERP.Models.HRM.Education", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<Guid>("EmployeeId");

                    b.Property<string>("Grade");

                    b.Property<string>("Institution");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("QualificationObtained");

                    b.Property<string>("Year");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Education");
                });

            modelBuilder.Entity("HRM.ERP.Models.HRM.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountNo")
                        .HasMaxLength(10);

                    b.Property<string>("AccountType");

                    b.Property<string>("Address");

                    b.Property<DateTime>("DOB");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("Email");

                    b.Property<string>("EmployeeId");

                    b.Property<string>("FirstName");

                    b.Property<string>("Gender");

                    b.Property<bool>("IsDeleted");

                    b.Property<Guid>("JobId");

                    b.Property<DateTime>("JoinDate");

                    b.Property<string>("LastName");

                    b.Property<string>("MaritalStatus");

                    b.Property<string>("MiddleName");

                    b.Property<string>("MobileNo")
                        .HasMaxLength(10);

                    b.Property<string>("Nationality");

                    b.Property<string>("Photograph");

                    b.Property<string>("SortCode");

                    b.Property<string>("Status");

                    b.Property<string>("WorkTelephone");

                    b.Property<string>("contractType");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("HRM.ERP.Models.HRM.Job", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ContractEndDate");

                    b.Property<DateTime>("ContractStartDate");

                    b.Property<Guid>("ContractTypeId");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("JobDescription");

                    b.Property<Guid>("JobTitleId");

                    b.Property<Guid>("OrganisationId");

                    b.HasKey("Id");

                    b.HasIndex("ContractTypeId");

                    b.HasIndex("JobTitleId");

                    b.ToTable("Job");
                });

            modelBuilder.Entity("HRM.ERP.Models.HRM.JobTitle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeleted");

                    b.Property<Guid>("OrganisationId");

                    b.Property<string>("TitleName");

                    b.HasKey("Id");

                    b.ToTable("JobTitle");
                });

            modelBuilder.Entity("HRM.ERP.Models.HRM.Language", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<Guid>("EmployeeId");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LanguageName");

                    b.Property<string>("Level");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Language");
                });

            modelBuilder.Entity("HRM.ERP.Models.HRM.Leave", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("Description");

                    b.Property<Guid>("EmployeeId");

                    b.Property<bool>("IsApproved");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("LeaveApprovedDate");

                    b.Property<DateTime>("LeaveEndDate");

                    b.Property<DateTime>("LeaveStartDate");

                    b.Property<string>("LeaveTitle");

                    b.Property<string>("Status");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Leave");
                });

            modelBuilder.Entity("HRM.ERP.Models.HRM.PayGrade", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("GradeName");

                    b.Property<bool>("IsDeleted");

                    b.Property<Guid>("JobId");

                    b.Property<Guid>("OrganisationId");

                    b.Property<double>("Price");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.ToTable("PayGrade");
                });

            modelBuilder.Entity("HRM.ERP.Models.HRM.Skill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<Guid>("EmployeeId");

                    b.Property<string>("Institution");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Level");

                    b.Property<string>("QualificationObtained");

                    b.Property<string>("Year");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Skill");
                });

            modelBuilder.Entity("HRM.ERP.Models.HRM.Vacancy", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("Description");

                    b.Property<string>("HiringManager");

                    b.Property<bool>("IsDeleted");

                    b.Property<Guid>("JobTitleId");

                    b.Property<int>("NoOfPositions");

                    b.Property<Guid>("OrganisationId");

                    b.Property<string>("VacancyName");

                    b.HasKey("Id");

                    b.HasIndex("JobTitleId");

                    b.ToTable("Vacancy");
                });

            modelBuilder.Entity("HRM.ERP.Models.Organisation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<Guid>("ApplicationUserId");

                    b.Property<string>("ApplicationUserId1");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("Email");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("NoOfEmployees");

                    b.Property<string>("OrganisationName");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("RegistrationNo");

                    b.Property<string>("State");

                    b.Property<string>("TaxId");

                    b.Property<int>("ZipCode");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId1");

                    b.ToTable("Organisation");
                });

            modelBuilder.Entity("HRM.ERP.Models.OtherUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<Guid>("HostId");

                    b.Property<bool>("IsDeleted");

                    b.Property<Guid>("OrganisationId");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.ToTable("OtherUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("HRM.ERP.Models.ACC.Customer", b =>
                {
                    b.HasOne("HRM.ERP.Models.Organisation", "Organisation")
                        .WithMany()
                        .HasForeignKey("OrganisationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HRM.ERP.Models.ACC.Estimate", b =>
                {
                    b.HasOne("HRM.ERP.Models.ACC.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HRM.ERP.Models.ACC.ItemsEIR", b =>
                {
                    b.HasOne("HRM.ERP.Models.ACC.Estimate", "Estimate")
                        .WithMany()
                        .HasForeignKey("EstimateId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HRM.ERP.Models.ACC.ProductService", b =>
                {
                    b.HasOne("HRM.ERP.Models.Organisation", "Organisation")
                        .WithMany()
                        .HasForeignKey("OrganisationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HRM.ERP.Models.HRM.Candidates", b =>
                {
                    b.HasOne("HRM.ERP.Models.HRM.Vacancy", "Vacancy")
                        .WithMany()
                        .HasForeignKey("VacancyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HRM.ERP.Models.HRM.Education", b =>
                {
                    b.HasOne("HRM.ERP.Models.HRM.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HRM.ERP.Models.HRM.Employee", b =>
                {
                    b.HasOne("HRM.ERP.Models.HRM.Job", "Job")
                        .WithMany()
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HRM.ERP.Models.HRM.Job", b =>
                {
                    b.HasOne("HRM.ERP.Models.HRM.ContractType", "ContractType")
                        .WithMany()
                        .HasForeignKey("ContractTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HRM.ERP.Models.HRM.JobTitle", "JobTitle")
                        .WithMany()
                        .HasForeignKey("JobTitleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HRM.ERP.Models.HRM.Language", b =>
                {
                    b.HasOne("HRM.ERP.Models.HRM.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HRM.ERP.Models.HRM.Leave", b =>
                {
                    b.HasOne("HRM.ERP.Models.HRM.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HRM.ERP.Models.HRM.PayGrade", b =>
                {
                    b.HasOne("HRM.ERP.Models.HRM.Job", "Job")
                        .WithMany()
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HRM.ERP.Models.HRM.Skill", b =>
                {
                    b.HasOne("HRM.ERP.Models.HRM.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HRM.ERP.Models.HRM.Vacancy", b =>
                {
                    b.HasOne("HRM.ERP.Models.HRM.JobTitle", "JobTitle")
                        .WithMany()
                        .HasForeignKey("JobTitleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HRM.ERP.Models.Organisation", b =>
                {
                    b.HasOne("HRM.ERP.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId1");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("HRM.ERP.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("HRM.ERP.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HRM.ERP.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("HRM.ERP.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
