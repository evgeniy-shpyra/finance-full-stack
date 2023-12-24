using Microsoft.EntityFrameworkCore;
using finance.DLL.Models;
using System.Linq;
using Microsoft.Extensions.Options;

namespace finance.DLL
{
    public class FinanceContext : DbContext
    {
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<FinancialCategory> FinancialCategories { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }
        public FinanceContext(DbContextOptions<FinanceContext> options) : base(options)
        {
           
            Database.EnsureCreated();

            if (!FinancialCategories.Any())
            {
                var systemCategory = new FinancialCategory { Name = "Transaction between wallet" };
                FinancialCategories.Add(systemCategory);

                var replenishmentTransactionType = new TransactionType { Name = "replenishment" };
                var betweenWalletsTransactionType = new TransactionType { Name = "between wallets" };
                var withdrawalType = new TransactionType { Name = "withdrawal" };
                TransactionTypes.AddRange(replenishmentTransactionType, betweenWalletsTransactionType, withdrawalType);

                var systemSendingWallet = new Wallet { Name = "systemSendingWallet" };
                var systemReceivingWallet = new Wallet { Name = "systemReceivingWallet" };

                Wallets.AddRange(systemSendingWallet, systemReceivingWallet);

                SaveChanges();
            }
         
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransactionType>()
                .HasKey(tt => tt.Id);
            modelBuilder.Entity<TransactionType>()
                .Property(tt => tt.Name)
                .IsRequired();
            modelBuilder.Entity<TransactionType>()
                .HasMany(tt => tt.Transactions)
                .WithOne(t => t.TransactionType)
                .HasForeignKey(t => t.TransactionTypeId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<TransactionType>()
                .HasMany(tt => tt.Historys)
                .WithOne(t => t.TransactionType)
                .HasForeignKey(t => t.TransactionTypeId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<FinancialCategory>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<FinancialCategory>()
                .Property(t => t.Name)
                .IsRequired();
            modelBuilder.Entity<FinancialCategory>()
                .HasMany(tt => tt.Transactions)
                .WithOne(t => t.FinancialCategory)
                .HasForeignKey(t => t.FinancialCategoryId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<History>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<History>()
                .Property(h => h.LeftBalance)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<History>()
              .Property(h => h.Price)
              .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<History>()
                .Property(h => h.WalletToName);
              modelBuilder.Entity<History>()
                .Property(h => h.WalletFromName);
            modelBuilder.Entity<History>()
                .Property(h => h.CategoryName);
            modelBuilder.Entity<History>()
                .HasOne(t => t.TransactionType)
                .WithMany(tt => tt.Historys)
                .HasForeignKey(t => t.TransactionTypeId)
                .OnDelete(DeleteBehavior.Restrict);
         

            modelBuilder.Entity<Transaction>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<Transaction>()
                .Property(t => t.Price)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.FinancialCategory)
                .WithMany(fc => fc.Transactions)
                .HasForeignKey(t => t.FinancialCategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.SendingWallet)
                .WithMany(fc => fc.SendTransactions)
                .HasForeignKey(t => t.SendingWalletId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Transaction>()
               .HasOne(t => t.ReceivingWallet)
               .WithMany(fc => fc.ReceivedTransactions)
               .HasForeignKey(t => t.ReceivingWalletId)
               .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.TransactionType)
                .WithMany(tt => tt.Transactions)
                .HasForeignKey(t => t.TransactionTypeId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Wallet>()
                .HasMany(w => w.SendTransactions)
                .WithOne(t => t.SendingWallet)
                .HasForeignKey(t => t.SendingWalletId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Wallet>()
                .HasMany(w => w.ReceivedTransactions)
                .WithOne(t => t.ReceivingWallet)
                .HasForeignKey(t => t.ReceivingWalletId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Wallet>()
                .Property(w => w.Balance)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);
        }
    }
}
