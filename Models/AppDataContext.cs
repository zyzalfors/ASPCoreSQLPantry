using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Pantry.Models
{
    public partial class AppDataContext : DbContext
    {

        public virtual DbSet<Pantry> Pantries { get; set; }

        public virtual DbSet<Package> Packages { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Pantry>(entity =>
            {
                entity.Property(e => e.PantryDesc)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductDesc)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PackageID).HasColumnType("int")
                    .IsRequired();
            });

            modelBuilder.Entity<Package>(entity =>
            {
                entity.Property(e => e.PackageDesc);

                entity.Property(e => e.PackageInteg).HasColumnType("int")
                    .IsRequired();

                entity.Property(e => e.PantryID).HasColumnType("int")
                    .IsRequired();

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
