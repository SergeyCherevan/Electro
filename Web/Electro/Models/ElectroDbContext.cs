using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Electro.Models
{
    public class ElectroDbContext : IdentityDbContext<User>
    {
        public ElectroDbContext(DbContextOptions<ElectroDbContext> options) : base(options) { }


        public DbSet<Electrocar> Electrocars { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Electrocar>()
                .HasOne(ec => ec.Owner)
                .WithMany(u => u.Electrocars);

            builder.Entity<User>()
                .HasMany(u => u.Electrocars)
                .WithOne(ec => ec.Owner);
        }
    }
}
