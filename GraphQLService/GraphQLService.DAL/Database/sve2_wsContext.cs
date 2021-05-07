using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace GraphQLService.DAL.Database
{
    public partial class sve2_wsContext : DbContext
    {
        public sve2_wsContext()
        {
        }

        public sve2_wsContext(DbContextOptions<sve2_wsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Lab> Labs { get; set; }
        public virtual DbSet<List> Lists { get; set; }
        public virtual DbSet<Point> Points { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Series> Series { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("server=localhost;database=sve2_ws;uid=root");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lab>(entity =>
            {
                entity.HasKey(e => e.IdLab)
                    .HasName("PRIMARY");

                entity.ToTable("labs");

                entity.Property(e => e.IdLab).HasColumnType("int(11)");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Timestamp).HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<List>(entity =>
            {
                entity.HasKey(e => new { e.IdList, e.IdProject, e.IdLab })
                    .HasName("PRIMARY");

                entity.ToTable("lists");

                entity.HasIndex(e => new { e.IdLab, e.IdProject }, "FK_lists_projects");

                entity.Property(e => e.IdList).HasColumnType("int(11)");

                entity.Property(e => e.IdProject).HasColumnType("int(11)");

                entity.Property(e => e.IdLab).HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Timestamp).HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<Point>(entity =>
            {
                entity.HasKey(e => new { e.IdLab, e.IdProject, e.IdPoint, e.IdSeries })
                    .HasName("PRIMARY");

                entity.ToTable("points");

                entity.HasIndex(e => new { e.IdLab, e.IdProject, e.IdList }, "FK_points_lists");

                entity.HasIndex(e => new { e.IdLab, e.IdProject, e.IdSeries }, "FK_points_series");

                entity.Property(e => e.IdLab).HasColumnType("int(11)");

                entity.Property(e => e.IdProject).HasColumnType("int(11)");

                entity.Property(e => e.IdPoint).HasColumnType("int(11)");

                entity.Property(e => e.IdSeries).HasColumnType("int(11)");

                entity.Property(e => e.IdList)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Timestamp).HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.Id)
                    .WithMany(p => p.Points)
                    .HasForeignKey(d => new { d.IdLab, d.IdProject, d.IdSeries })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_points_series");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => new { e.IdProject, e.IdLab })
                    .HasName("PRIMARY");

                entity.ToTable("projects");

                entity.HasIndex(e => e.IdLab, "FK_projects_labs");

                entity.Property(e => e.IdProject).HasColumnType("int(11)");

                entity.Property(e => e.IdLab).HasColumnType("int(11)");

                entity.Property(e => e.Customer)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Timestamp).HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.IdLabNavigation)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.IdLab)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_projects_labs");
            });

            modelBuilder.Entity<Series>(entity =>
            {
                entity.HasKey(e => new { e.IdLab, e.IdProject, e.IdSeries })
                    .HasName("PRIMARY");

                entity.ToTable("series");

                entity.Property(e => e.IdLab).HasColumnType("int(11)");

                entity.Property(e => e.IdProject).HasColumnType("int(11)");

                entity.Property(e => e.IdSeries).HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Timestamp).HasDefaultValueSql("'NULL'");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
