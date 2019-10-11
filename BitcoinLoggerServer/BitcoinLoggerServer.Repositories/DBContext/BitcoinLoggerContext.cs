using System;
using BitcoinLoggerServer.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BitcoinLoggerServer.Repositories.DBContext
{
    public partial class BitcoinLoggerContext : DbContext
    {
        private readonly string _ConnectionString;

        public BitcoinLoggerContext(string connectionString)
        {
            _ConnectionString = connectionString;
        }

        public BitcoinLoggerContext(DbContextOptions<BitcoinLoggerContext> options, string connectionString) : base(options)
        {
            _ConnectionString = connectionString;
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Users__A9D10534893090B8")
                    .IsUnique();

                entity.Property(e => e.DateCreated).HasColumnType("datetime2(0)");

                entity.Property(e => e.DateModified).HasColumnType("datetime2(0)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Phone).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
