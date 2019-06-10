using Microsoft.EntityFrameworkCore;
using Panda.Models;

namespace Panda.Data
{
    public class PandaDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Package> Packages { get; set; }

        public DbSet<Receipt> Receipts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(user => user.Id);
            modelBuilder.Entity<User>().HasMany(user => user.Packages).WithOne(package => package.Recipient)
                .HasForeignKey(package => package.RecipientId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>().HasMany(user => user.Receipts).WithOne(receipt => receipt.Recipient)
                .HasForeignKey(receipt => receipt.RecipientId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Package>().HasKey(package => package.Id);

            modelBuilder.Entity<Receipt>().HasKey(receipt => receipt.Id);
            modelBuilder.Entity<Receipt>().HasOne(receipt => receipt.Package);
        }
    }
}