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
            SeedCity(services);
            SeedConstruction(services);

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

        private static void SeedCity(IServiceProvider service) 
        {
            var data = service.GetRequiredService<BuyHouseDbContext>();

            if (data.Cities.Any()) 
            {
                return;
            }

            data.Cities.AddRange(new[]
            {
                new City { Name = "Sofia"},
                new City { Name = "Plovdiv"},
                new City { Name = "Varna"},
                new City { Name = "Burgas"},
                new City { Name = "BlagoevGrad"},
                new City { Name = "Sandansky"}
            });

            data.SaveChanges();
        }

        private static void SeedConstruction(IServiceProvider service) 
        {
            var data = service.GetRequiredService<BuyHouseDbContext>();

            if (data.Constructions.Any()) 
            {
                return;
            }

            data.Constructions.AddRange(new[]
            {
                new Construction { Name = "Panel"},
                new Construction { Name = "Brick"},
                new Construction { Name = "YTONG"},
                new Construction { Name = "EPK"}
            });

            data.SaveChanges();
        }
    }
}
