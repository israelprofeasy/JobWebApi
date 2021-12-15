using JobListing.Models.EFModels;
using JobListingAppUI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace JobListingAppUI.DbContexts
{
    public class JobListingDbContext : IdentityDbContext<AppUser>
    {
        public JobListingDbContext(DbContextOptions<JobListingDbContext> options) : base(options)
        {

        }
        
        public DbSet<Job> Jobs { get; set; } 
        public DbSet<JobCategory> Category { get; set; } 
        public DbSet<JobIndustry> Industry { get; set; }
        //  public DbSet<DocumentUpload> JobApplied { get; set; }




        // public DbSet<ApplicantUser> Applicants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Job>().Property(p => p.MinimumSalary).HasColumnType("decimal(18,4)");
            modelBuilder.Entity<Job>().Property(p => p.MaximumSalary).HasColumnType("decimal(18,4)");
        }

    }
}
