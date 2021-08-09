namespace BuyHouse.Infrastructure
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using BuyHouse.Data;
    using BuyHouse.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using static WebConstants;
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app) 
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);
            SeedCategory(services);
            SeedAdministrator(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider service) 
        {
            var data = service.GetRequiredService<BuyHouseDbContext>();

            data.Database.Migrate();
        }

        private static void SeedCategory(IServiceProvider service) 
        {
            var data = service.GetRequiredService<BuyHouseDbContext>();

            if (data.Categories.Any()) 
            {
                return;
            }

            data.Categories.AddRange(new[]
            {
                new Category { Name = "House"},
                new Category { Name = "Apartment"},
                new Category { Name = "CountryHouse"},
                new Category { Name = "Office"},
                new Category { Name = "Shop"},
                new Category { Name = "Garage"},
                new Category { Name = "Basement"}
            });

            data.SaveChanges();
        }

        private static void SeedAdministrator(IServiceProvider serviceProvider) 
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            Task.Run(async () =>
            {
                if (await roleManager.RoleExistsAsync(AdministratolRoleName)) 
                {
                    return;
                }

                var role = new IdentityRole { Name = AdministratolRoleName };
                await roleManager.CreateAsync(role);

                const string adminEmail = "admin@abv.bg";
                const string adminPasword = "1234567";

                var user = new User
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    FullName = "Admin"
                };

                await userManager.CreateAsync(user, adminPasword);

                await userManager.AddToRoleAsync(user, role.Name);
            })
                .GetAwaiter()
                .GetResult();
        }
    }
}
