using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HRM.ERP.Data;
using HRM.ERP.Models;
using HRM.ERP.Services;

namespace HRM.ERP
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            try
            {
                CreateRoles(serviceProvider).Wait();
            }
            catch
            {

            }
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //initializing custom roles   
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
           // var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            string[] roleNames = { "Super Admin", "Admin", "User", "Employee" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 1  
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //IdentityUser user = await UserManager.FindByEmailAsync("eadewuyi@wragbysolutions.com");

            //if (user == null)
            //{
            //    user = new IdentityUser()
            //    {
            //        UserName = "eadewuyi@wragbysolutions.com",
            //        Email = "eadewuyi@wragbysolutions.com",
                    
            //    };
            //    await UserManager.CreateAsync(user, "GE8hVBar.");
            //}
            //await UserManager.AddToRoleAsync(user, "SuperAdmin");


            //IdentityUser user1 = await UserManager.FindByEmailAsync("tejas@gmail.com");

            //if (user1 == null)
            //{
            //    user1 = new IdentityUser()
            //    {
            //        UserName = "tejas@gmail.com",
            //        Email = "tejas@gmail.com",
            //    };
            //    await UserManager.CreateAsync(user1, "Test@123");
            //}
            //await UserManager.AddToRoleAsync(user1, "User");

            //IdentityUser user2 = await UserManager.FindByEmailAsync("rakesh@gmail.com");

            //if (user2 == null)
            //{
            //    user2 = new IdentityUser()
            //    {
            //        UserName = "rakesh@gmail.com",
            //        Email = "rakesh@gmail.com",
            //    };
            //    await UserManager.CreateAsync(user2, "Test@123");
            //}
            //await UserManager.AddToRoleAsync(user2, "HR");

        }
    }
}
