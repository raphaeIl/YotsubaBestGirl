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
            modelBuilder.Entity<CardDB>()
                .HasOne(c => c.User)
                .WithMany(u => u.Cards)
                .HasForeignKey(c => c.Uid)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
