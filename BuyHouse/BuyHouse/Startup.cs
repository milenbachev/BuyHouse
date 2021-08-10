namespace BuyHouse
{
    using BuyHouse.Areas.Admin.Services.Categories;
    using BuyHouse.Data;
    using BuyHouse.Data.Models;
    using BuyHouse.Infrastructure;
    using BuyHouse.Services.Agents;
    using BuyHouse.Services.Home;
    using BuyHouse.Services.Issues;
    using BuyHouse.Services.Properties;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<BuyHouseDbContext>(options => options
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services
                .AddDatabaseDeveloperPageExceptionFilter();

            services
                .AddDefaultIdentity<User>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<BuyHouseDbContext>();

            services
                .AddControllersWithViews(option => 
                {
                    option.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
                });

            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IHomeService, HomeService>();
            services.AddTransient<IAgentService, AgentService>();
            services.AddTransient<IIssueService, IssueService>();
            services.AddTransient<IPropertyService, PropertyService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.PrepareDatabase();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultAreaRoute();
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
        }
    }
}
