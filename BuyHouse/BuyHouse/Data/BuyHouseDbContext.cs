namespace BuyHouse.Data
{
    using BuyHouse.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class BuyHouseDbContext : IdentityDbContext
    {
        public BuyHouseDbContext(DbContextOptions<BuyHouseDbContext> options)
            : base(options)
        {
        }

        public DbSet<Agent> Agents { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Construction> Constructions { get; set; }

        public DbSet<Property> Properties { get; set; }

        public DbSet<TypeOfTransaction> TypeOfTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Property>()
                .HasOne(x => x.Category)
                .WithMany(x => x.Properties)
                .HasForeignKey(x => x.CategotyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Property>()
                .HasOne(x => x.Construction)
                .WithMany(x => x.Properties)
                .HasForeignKey(x => x.ConstructionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Property>()
                .HasOne(x => x.Agent)
                .WithMany(x => x.Properties)
                .HasForeignKey(x => x.AgentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Property>()
                .HasOne(x => x.TypeOfTransaction)
                .WithMany(x => x.Properties)
                .HasForeignKey(x => x.TypeOfTransactionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Property>()
                .HasOne(x => x.City)
                .WithMany(x => x.Properties)
                .HasForeignKey(x => x.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Agent>()
                .HasOne(x => x.City)
                .WithMany(x => x.Agents)
                .HasForeignKey(x => x.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
