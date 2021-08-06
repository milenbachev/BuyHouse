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

        public DbSet<Property> Properties { get; set; }

        public DbSet<Issue> Issues { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Property>()
                .HasOne(x => x.Category)
                .WithMany(x => x.Properties)
                .HasForeignKey(x => x.CategotyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Property>()
                .HasOne(x => x.Agent)
                .WithMany(x => x.Properties)
                .HasForeignKey(x => x.AgentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Issue>()
                .HasOne(x => x.Property)
                .WithMany(x => x.Issues)
                .HasForeignKey(x => x.PropertyId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
