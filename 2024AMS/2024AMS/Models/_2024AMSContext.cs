using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace _2024AMS.Models
{
    public partial class _2024AMSContext : DbContext
    {
        public _2024AMSContext()
        {
        }

        public _2024AMSContext(DbContextOptions<_2024AMSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Asset> Asset { get; set; } = null!;
        public virtual DbSet<AssetCategory> AssetCategory { get; set; } = null!;
        public virtual DbSet<Manufacturer> Manufacturer { get; set; } = null!;
        public virtual DbSet<OperatingSystem> OperatingSystem { get; set; } = null!;
        public virtual DbSet<User> User { get; set; } = null!;
        public virtual DbSet<UserAsset> UserAsset { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-3AMES46\\SQLEXPRESS; Database=2024AMS; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>(entity =>
            {
                entity.Property(e => e.Asset1)
                    .HasMaxLength(50)
                    .HasColumnName("Asset");

                entity.Property(e => e.Barcode).HasMaxLength(13);

                entity.Property(e => e.MACAddress).HasMaxLength(20);

                entity.Property(e => e.Model).HasMaxLength(50);

                entity.Property(e => e.PurchaseDate).HasColumnType("date");

                entity.Property(e => e.SerialNumber).HasMaxLength(50);

                entity.Property(e => e.ServiceTag).HasMaxLength(50);

                entity.Property(e => e.WarrantyDate).HasColumnType("date");

                entity.HasOne(d => d.AssetCategory)
                    .WithMany(p => p.Asset)
                    .HasForeignKey(d => d.AssetCategoryID)
                    .HasConstraintName("FK_Asset_AssetCategory");

                entity.HasOne(d => d.Manufacturer)
                    .WithMany(p => p.Asset)
                    .HasForeignKey(d => d.ManufacturerID)
                    .HasConstraintName("FK_Asset_Manufacturer");

                entity.HasOne(d => d.OperatingSystem)
                    .WithMany(p => p.Asset)
                    .HasForeignKey(d => d.OperatingSystemID)
                    .HasConstraintName("FK_Asset_OperatingSystem");
            });

            modelBuilder.Entity<AssetCategory>(entity =>
            {
                entity.Property(e => e.AssetCategory1)
                    .HasMaxLength(50)
                    .HasColumnName("AssetCategory");
            });

            modelBuilder.Entity<Manufacturer>(entity =>
            {
                entity.Property(e => e.Manufacturer1)
                    .HasMaxLength(50)
                    .HasColumnName("Manufacturer");
            });

            modelBuilder.Entity<OperatingSystem>(entity =>
            {
                entity.Property(e => e.OperatingSystem1)
                    .HasMaxLength(50)
                    .HasColumnName("OperatingSystem");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.EmailAddress).HasMaxLength(128);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.MiddleInitial).HasMaxLength(1);

                entity.Property(e => e.Status).HasMaxLength(1);

            });

            modelBuilder.Entity<UserAsset>(entity =>
            {
                entity.Property(e => e.CheckedInDate).HasColumnType("date");

                entity.Property(e => e.CheckedOutDate).HasColumnType("date");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.UserAsset)
                    .HasForeignKey(d => d.AssetID)
                    .HasConstraintName("FK_UserAsset_Asset");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAsset)
                    .HasForeignKey(d => d.UserID)
                    .HasConstraintName("FK_UserAsset_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
