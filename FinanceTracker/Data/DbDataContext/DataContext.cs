using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace FinanceTracker.Data.DbDataContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
                
        }


        
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category>  Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>().
            //    HasMany<Budget>().WithOne(u => u.User).
            //    HasForeignKey(u => u.UserId).
            //    OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Budget>().HasOne(uc => uc.User)
              .WithMany(u => u.Budgets)
              .HasForeignKey(uc => uc.UserId)
              .IsRequired()
              .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Transaction>().HasOne(uc => uc.Account)
            //.WithMany(u => u.Transaction)
            //.HasForeignKey(uc => uc.AccountId)
            //.IsRequired()
            //.OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Transaction>()
            //    .HasKey(r => r.Id);


            modelBuilder.Entity<Account>()
           .HasOne(i => i.User)
           .WithMany(c => c.Accounts)
           .IsRequired()
           .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Transaction>()
           .HasOne(i => i.Account)
           .WithMany(c => c.Transaction)
           .IsRequired()
           .OnDelete(DeleteBehavior.Cascade);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
    