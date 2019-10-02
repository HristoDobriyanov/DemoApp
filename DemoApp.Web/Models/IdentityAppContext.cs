using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Web.Models
{
    public class IdentityAppContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public IdentityAppContext(DbContextOptions<IdentityAppContext> options) : base(options)
        {

        }
        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<Contragent> Contragents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>()
                .HasMany<Contragent>(c => c.Contragents)
                .WithOne(u => u.AppUser);

        }
    }
}

