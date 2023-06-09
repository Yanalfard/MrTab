﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DataLayer.Models
{
    public partial class MrTabContext : DbContext
    {
        public MrTabContext()
        {
        }

        public MrTabContext(DbContextOptions<MrTabContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblCatagory> TblCatagory { get; set; }
        public virtual DbSet<TblCity> TblCity { get; set; }
        public virtual DbSet<TblComment> TblComment { get; set; }
        public virtual DbSet<TblConfig> TblConfig { get; set; }
        public virtual DbSet<TblDoc> TblDoc { get; set; }
        public virtual DbSet<TblFood> TblFood { get; set; }
        public virtual DbSet<TblFoodType> TblFoodType { get; set; }
        public virtual DbSet<TblImage> TblImage { get; set; }
        public virtual DbSet<TblMealType> TblMealType { get; set; }
        public virtual DbSet<TblProperty> TblProperty { get; set; }
        public virtual DbSet<TblReport> TblReport { get; set; }
        public virtual DbSet<TblRestaurant> TblRestaurant { get; set; }
        public virtual DbSet<TblRole> TblRole { get; set; }
        public virtual DbSet<TblUser> TblUser { get; set; }
        public virtual DbSet<TblWorkTime> TblWorkTime { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
=> optionsBuilder
.UseLazyLoadingProxies()
.UseSqlServer("Data Source=193.141.64.241,2019;Initial Catalog=mrfoodt1_MrFood;User ID=Yanal;Password=s*42Lh5p1");
        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseSqlServer("Data Source=185.55.224.183;Initial Catalog=asamedc1_Foodestan;User ID=asamedc1_Yanal2;Password=2fjS9CYVYkgS5V8");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:DefaultSchema", "asamedc1_Yanal2");

            modelBuilder.Entity<TblComment>(entity =>
            {
                entity.Property(e => e.DecorRate).HasDefaultValueSql("((0))");

                entity.Property(e => e.ExpenseRate).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsReported).HasDefaultValueSql("((0))");

                entity.Property(e => e.LikeCount).HasDefaultValueSql("((0))");

                entity.Property(e => e.QualityPerPriceRate).HasDefaultValueSql("((0))");

                entity.Property(e => e.QualityRate).HasDefaultValueSql("((0))");

                entity.Property(e => e.Rate).HasDefaultValueSql("((0))");

                entity.Property(e => e.ServiceRate).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Answer)
                    .WithMany(p => p.InverseAnswer)
                    .HasForeignKey(d => d.AnswerId)
                    .HasConstraintName("FK_TblComment_TblComment");

                entity.HasOne(d => d.Doc)
                    .WithMany(p => p.TblComment)
                    .HasForeignKey(d => d.DocId)
                    .HasConstraintName("FK_TblComment_TblDoc");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.TblComment)
                    .HasForeignKey(d => d.RestaurantId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TblComment_TblRestaurant");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblComment)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_TblComment_TblUser");
            });

            modelBuilder.Entity<TblFood>(entity =>
            {
                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.TblFood)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK_TblFood_TblRestaurant");
            });

            modelBuilder.Entity<TblFoodType>(entity =>
            {
                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.TblFoodType)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK_TblFoodType_TblRestaurant");
            });

            modelBuilder.Entity<TblImage>(entity =>
            {
                entity.Property(e => e.Status).HasComment("0 is Normal; 1 is Menu");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.TblImage)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK_TblImage_TblRestaurant");
            });

            modelBuilder.Entity<TblMealType>(entity =>
            {
                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.TblMealType)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK_TblMealType_TblRestaurant");
            });

            modelBuilder.Entity<TblProperty>(entity =>
            {
                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.TblProperty)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK_TblProperty_TblRestaurant");
            });

            modelBuilder.Entity<TblReport>(entity =>
            {
                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.TblReport)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK_TblReport_TblRestaurant");
            });

            modelBuilder.Entity<TblRestaurant>(entity =>
            {
                entity.HasOne(d => d.Catagory)
                    .WithMany(p => p.TblRestaurant)
                    .HasForeignKey(d => d.CatagoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblRestaurant_TblCatagory");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.TblRestaurant)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblRestaurant_TblCity");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblRestaurant)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblRestaurant_TblUser");
            });

            modelBuilder.Entity<TblRole>(entity =>
            {
                entity.Property(e => e.RoleId).ValueGeneratedNever();

                entity.Property(e => e.Title).IsFixedLength();
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TblUser)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblUser_TblRole");
            });

            modelBuilder.Entity<TblWorkTime>(entity =>
            {
                entity.HasKey(e => e.WorkTimeId)
                    .HasName("PK_TblWorkTimes");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.TblWorkTime)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK_TblWorkTimes_TblRestaurant");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
