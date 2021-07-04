using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using GRR.Data.Models;

#nullable disable

namespace GRR.Data.BaseContext
{
    public partial class GRRContext : DbContext
    {
        public GRRContext()
        {
        }

        public GRRContext(DbContextOptions<GRRContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DbUser> DbUsers { get; set; }
        public virtual DbSet<UserAdditionalDetail> UserAdditionalDetails { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=GRR");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<DbUser>(entity =>
            {
                entity.ToTable("DbUser");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Dob)
                    .HasColumnType("datetime")
                    .HasColumnName("DOB");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<UserAdditionalDetail>(entity =>
            {
                entity.ToTable("UserAdditionalDetail");

                entity.HasIndex(e => e.UserId, "IX_UserId")
                    .IsUnique()
                    .HasFilter("([UserId] IS NOT NULL)");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Groups).HasMaxLength(100);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.UserAdditionalDetail)
                    .HasForeignKey<UserAdditionalDetail>(d => d.UserId)
                    .HasConstraintName("FK_UserAdditionalDetails_DbUser_UserId");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Roles).HasMaxLength(100);

                entity.HasOne(d => d.UserAdditionalDetail)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserAdditionalDetailId)
                    .HasConstraintName("FK_UserRole_UserAdditionalDetail_UserId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
