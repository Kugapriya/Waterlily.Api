using System;
using Microsoft.EntityFrameworkCore;
using Waterlily.Api.Entities;

namespace Waterlily.Api.Data;


public class DataContext(DbContextOptions<DataContext> options) : DbContext(options) 
    {
        public DbSet<Employee> Employees{get;set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
              modelBuilder.Entity<Employee>().ToTable("Employees", "WaterLilyLabs");

            base.OnModelCreating(modelBuilder);
        }
}

