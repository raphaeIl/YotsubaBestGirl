using System;

using Microsoft.EntityFrameworkCore;
using YotsubaBestGirl.Database.Entities;

namespace YotsubaBestGirl.Database
{
    public class YotsubaContext : DbContext
    {
        public DbSet<PlayerAccountDB> PlayerAccounts { get; set; }

        public DbSet<UserDB> Users { get; set; }
        public DbSet<CardDB> Cards { get; set; }

        public YotsubaContext(DbContextOptions<YotsubaContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDB>()
                .HasIndex(u => u.Uid)
                .IsUnique();

            // cascade behavior
            //modelBuilder.Entity<CardDB>()
            //    .HasOne(c => c.User)
            //    .WithMany(u => u.Cards)
            //    .HasForeignKey(c => c.Uid)
            //    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserDB>(entity =>
            {
                entity.OwnsOne(p => p.Currency, currency =>
                {
                    currency.Property(a => a.PayCoin).HasColumnName("PayCoin");
                    currency.Property(a => a.FreeCoin).HasColumnName("FreeCoin");
                });
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
