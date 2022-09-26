using Banking.Domain;
using Microsoft.EntityFrameworkCore;

namespace Banking.Storage
{
    internal class AccountContext : DbContext
    {
        public AccountContext(DbContextOptions<AccountContext> options): base(options)
        { }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasIndex(account => account.AccountNumber)
                .IsUnique();

            modelBuilder.Entity<Account>()
                .Property(account => account.Balance)
                .IsRequired();

            modelBuilder.Entity<Account>()
                .Property(account => account.UserId)
                .IsRequired();

            //Shadow property
            modelBuilder.Entity<Account>()
                .Property<long>("ID")
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Account>().HasKey("ID");
            //
        }
    }
}
