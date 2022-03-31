using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuGiOhTeamApp.Entities
{
    public class AppDbContext : DbContext
    {
        private string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=AppDB;Trusted_Connection=True";
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<UserRequests> UserRequests { get; set; }
        public DbSet<Decklist> Decklists { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(25);
            modelBuilder.Entity<Team>()
                .Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<UserRequests>()
                .HasKey(ur => new { ur.UserId, ur.TeamId });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
