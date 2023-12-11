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
        //public DbSet<Transaction> Transactions { get; set; }
        //public DbSet<Budget> Budgets { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category>  Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Budget>().
                HasOne<User>().WithMany(u => u.Budgets).
                HasForeignKey(u => u.UserId).
                OnDelete(DeleteBehavior.Restrict);

           
        }
    }
}
