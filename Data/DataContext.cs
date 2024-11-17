using System;
using Microsoft.EntityFrameworkCore;
using Waterlily.Api.Entities;

namespace Waterlily.Api.Data;


public class DataContext(DbContextOptions<DataContext> options) : DbContext(options) 
    {
        public DbSet<Employee> Employees{get;set;}
        public DbSet<PublicHoliday> Holidays {get;set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
              modelBuilder.Entity<Employee>().ToTable("Employees", "WaterLilyLabs");
              modelBuilder.Entity<PublicHoliday>().ToTable("Holidays", "WaterLilyLabs");

            base.OnModelCreating(modelBuilder);
        }
}

