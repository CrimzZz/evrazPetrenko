using evraz.Data.DbEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace evraz.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Defect> Defects { get; set; }
        public DbSet<Brand> Brands { get; set; }

        public ApplicationDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=usersdb;Username=postgres;Password=postgres");
        }
    }
}