using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using _1500MentoriaSistema.Models;

namespace _1500MentoriaSistema.Data
{
    public class ApplicationDbContext : IdentityDbContext<Person, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Person> People { get; set; }
        public DbSet<Learning> Learnings { get; set; }
        public DbSet<PersonLearning> PeopleLearning { get; set; }
        public DbSet<PersonFeedback> PeopleFeedback { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Circle> Circle { get; set; }
        public DbSet<TypeConsultor> TypeConsultor { get; set; }
        public DbSet<ActualState> ActualStates { get; set; }
        public DbSet<HoursDay> HoursDay { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Captures> Captures { get; set; }
        public DbSet<CostCenter> CostCenter { get; set; }
        public DbSet<Enterprise> Enterprises { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ActualState>()
                    .HasOne(e => e.Person)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Name = "Employer", NormalizedName = "EMPLOYER" },
                new IdentityRole { Name = "Basic", NormalizedName = "BASIC" }
            );

            modelBuilder.Entity<PersonFeedback>()
                   .HasOne(e => e.Person)
                   .WithMany()
                   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PersonLearning>()
               .HasOne(e => e.Person)
               .WithMany()
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
