namespace BuyHouse.Infrastructure
{
    using BuyHouse.Data;
    using BuyHouse.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;


    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app) 
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);
            SeedCategory(services);
            SeedTransaction(services);

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

        private static void SeedTransaction(IServiceProvider service) 
        {
            var data = service.GetRequiredService<BuyHouseDbContext>();

            if (data.TypeOfTransactions.Any()) 
            {
                return;
            }

            data.TypeOfTransactions.AddRange(new[]
            {
                new TypeOfTransaction { Name = "For Sale"},
                new TypeOfTransaction { Name = "Renting Out"},
                new TypeOfTransaction {Name = "Buy"},
                new TypeOfTransaction { Name = "Rent"}
            });

            data.SaveChanges();
        }
    }
}
