using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Design;
using NewsFlow.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;


namespace NewsFlow.Data.NewsFlowDbContext
{
    public class RssDbContext : DbContext
    {

        public RssDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Feed>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<Feed>()
                .Property(f => f.TimestampAdded)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Feed>()
                .HasIndex(f => f.Link)
                .IsUnique();
        }

        public DbSet<Feed> Feeds { get; set; }
    }
}

