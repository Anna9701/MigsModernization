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
        public virtual DbSet<ModernizationType> ModernizationTypes { get; set; }
        public virtual DbSet<Airplane> Airplanes { get; set; }
        public virtual DbSet<StagingArea> StagingAreas { get; set; }

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

                entity.HasOne(e => e.AirplaneNavigation)
                    .WithMany(a => a.Migs)
                    .HasForeignKey(e => e.AirplaneId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("migs_fk0");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("text");

                entity.Property(e => e.StagingArea)
                    .IsRequired()
                    .HasColumnName("staging_area")
                    .HasColumnType("text");

                entity.Property(e => e.AirplaneId)
                    .IsRequired()
                    .HasColumnName("airplane_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Unit)
                    .IsRequired()
                    .HasColumnName("unit")
                    .HasColumnType("text");

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
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("modernization_fk1");
            });

            modelBuilder.Entity<ModernizationType>(entity =>
            {
                entity.ToTable("modernization_type");

                entity.HasIndex(e => e.Id).IsUnique(true);
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .IsRequired(true)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Name)
                    .IsRequired(true)
                    .HasColumnName("name")
                    .HasColumnType("text");

            });

            modelBuilder.Entity<Airplane>(entity =>
            {
                entity.ToTable("airplane");

                entity.HasIndex(e => e.Id).IsUnique(true);
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .IsRequired(true)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Name)
                    .IsRequired(true)
                    .HasColumnName("name")
                    .HasColumnType("text");

                entity.Property(e => e.Type)
                    .IsRequired(true)
                    .HasColumnName("type")
                    .HasColumnType("text");

                entity.Property(e => e.Version)
                    .IsRequired(true)
                    .HasColumnName("version")
                    .HasColumnType("text");

            });

            modelBuilder.Entity<StagingArea>(entity =>
            {
                entity.ToTable("staging_area");

                entity.HasIndex(e => e.Id).IsUnique(true);
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .IsRequired(true)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.CityName)
                    .IsRequired(true)
                    .HasColumnName("city_name")
                    .HasColumnType("text");

            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.ToTable("unit");

                entity.HasIndex(e => e.Id).IsUnique(true);
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .IsRequired(true)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Name)
                    .IsRequired(true)
                    .HasColumnName("name")
                    .HasColumnType("text");

            });
        }

        public DbSet<MigsModernization.Models.Unit> Units { get; set; }
    }
}
