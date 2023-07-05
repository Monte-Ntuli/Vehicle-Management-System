﻿using BlazorApp1.Server.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1
{
    public class VehicleDbContext : IdentityDbContext 
    {
        public virtual DbSet<AppUser> AppUsers { get; set; }  
        public virtual DbSet<EmployeeEntity> Employees { get; set; }
        public virtual DbSet<QuestionaireEntity> Questionaires { get; set; } 
        public virtual DbSet<VehicleTypeEntity> VehicleTypes { get; set; }
        public virtual DbSet<VehicleEntity> Vehicles { get; set; }
        public virtual DbSet<QuestionsEntity> Questions { get; set; }
        public virtual DbSet<VehicleModelTypeEntity> VehicleModelTypes { get; set; }
        public VehicleDbContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}