using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MigsModernization.Models;

namespace MigsModernization.Context
{
    public partial class Sin79_MigsModernizationContext : DbContext
    {
        public Sin79_MigsModernizationContext()
        {
        }

        public Sin79_MigsModernizationContext(DbContextOptions<Sin79_MigsModernizationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Mig> Migs { get; set; }
        public virtual DbSet<Modernization> Modernization { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.EnableSensitiveDataLogging(true);
                optionsBuilder.UseMySql("Server=sql.sin79.nazwa.pl;User Id=sin79_MigsModernization;Password=Simba_12;Database=sin79_MigsModernization");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mig>(entity =>
            {
                entity.HasIndex(e => e.SideNumber).IsUnique(true);

                entity.HasKey(e => e.SideNumber);

                entity.ToTable("migs");

                entity.Property(e => e.SideNumber)
                    .IsRequired(true)
                    .HasColumnName("side_number")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Airplane)
                    .IsRequired()
                    .HasColumnName("airplane")
                    .HasColumnType("text");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("text");

                entity.Property(e => e.StagingArea)
                    .IsRequired()
                    .HasColumnName("staging_area")
                    .HasColumnType("text");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasColumnType("text");

                entity.Property(e => e.Unit)
                    .IsRequired()
                    .HasColumnName("unit")
                    .HasColumnType("text");

                entity.Property(e => e.Version).HasColumnName("version");
            });

            modelBuilder.Entity<Modernization>(entity =>
            {
                entity.ToTable("modernization");

                entity.HasIndex(e => e.MigSideNumber)
                    .HasName("modernization_fk0")
                    .IsUnique(true);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.MigSideNumber)
                    .HasColumnName("mig_side_number")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.ModernizationName)
                    .IsRequired()
                    .HasColumnName("modernization_name")
                    .HasColumnType("text");

                entity.Property(e => e.Performed)
                    .HasColumnName("performed")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.PlannedBy)
                    .HasColumnName("planned_by")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.MigSideNumberNavigation)
                    .WithMany(p => p.Modernizations)
                    .HasForeignKey(d => d.MigSideNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("modernization_fk0");
            });
        }
    }
}
