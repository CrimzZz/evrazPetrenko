﻿using db.DbEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace db
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Defect> Defects { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<DefectData> DefectDatas { get; set; }
        public DbSet<Raport> Raports { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasPostgresExtension("uuid-ossp");
            builder.Entity<Product>().Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            builder.Entity<Raport>().HasMany(e => e.Products);
            base.OnModelCreating(builder);
        }
    }
}