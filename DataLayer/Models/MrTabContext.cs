using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

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

        public virtual DbSet<TblCatagory> TblCatagories { get; set; }
        public virtual DbSet<TblCity> TblCities { get; set; }
        public virtual DbSet<TblComment> TblComments { get; set; }
        public virtual DbSet<TblConfig> TblConfigs { get; set; }
        public virtual DbSet<TblFood> TblFoods { get; set; }
        public virtual DbSet<TblFoodType> TblFoodTypes { get; set; }
        public virtual DbSet<TblImage> TblImages { get; set; }
        public virtual DbSet<TblMealType> TblMealTypes { get; set; }
        public virtual DbSet<TblProperty> TblProperties { get; set; }
        public virtual DbSet<TblReport> TblReports { get; set; }
        public virtual DbSet<TblRestaurant> TblRestaurants { get; set; }
        public virtual DbSet<TblRole> TblRoles { get; set; }
        public virtual DbSet<TblUser> TblUsers { get; set; }
        public virtual DbSet<TblWorkTime> TblWorkTimes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
             => optionsBuilder
           .UseLazyLoadingProxies()
           .UseSqlServer("Data Source=103.216.62.27;Initial Catalog=MrTab;User ID=Yanal;Password=1710ahmad.fard");
        //  {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //                optionsBuilder.UseSqlServer("Data Source=103.216.62.27;Initial Catalog=MrTab;User ID=Yanal;Password=1710ahmad.fard");


        //            }



        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblComment>(entity =>
            {
                entity.HasOne(d => d.Answer)
                    .WithMany(p => p.InverseAnswer)
                    .HasForeignKey(d => d.AnswerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblComment_TblComment");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.TblComments)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK_TblComment_TblRestaurant");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblComments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblComment_TblUser");
            });

            modelBuilder.Entity<TblFood>(entity =>
            {
                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.TblFoods)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK_TblFood_TblRestaurant");
            });

            modelBuilder.Entity<TblFoodType>(entity =>
            {
                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.TblFoodTypes)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK_TblFoodType_TblRestaurant");
            });

            modelBuilder.Entity<TblImage>(entity =>
            {
                entity.Property(e => e.Status).HasComment("0 is Normal; 1 is Menu");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.TblImages)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK_TblImage_TblRestaurant");
            });

            modelBuilder.Entity<TblMealType>(entity =>
            {
                entity.Property(e => e.MealTypeId).ValueGeneratedNever();

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.TblMealTypes)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK_TblMealType_TblRestaurant");
            });

            modelBuilder.Entity<TblProperty>(entity =>
            {
                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.TblProperties)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK_TblProperty_TblRestaurant");
            });

            modelBuilder.Entity<TblReport>(entity =>
            {
                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.TblReports)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK_TblReport_TblRestaurant");
            });

            modelBuilder.Entity<TblRestaurant>(entity =>
            {
                entity.HasOne(d => d.Catagory)
                    .WithMany(p => p.TblRestaurants)
                    .HasForeignKey(d => d.CatagoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblRestaurant_TblCatagory");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.TblRestaurants)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblRestaurant_TblCity");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblRestaurants)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblRestaurant_TblUser");
            });

            modelBuilder.Entity<TblRole>(entity =>
            {
                entity.Property(e => e.RoleId).ValueGeneratedNever();

                entity.Property(e => e.Title).IsFixedLength(true);
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TblUsers)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblUser_TblRole");
            });

            modelBuilder.Entity<TblWorkTime>(entity =>
            {
                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.TblWorkTimes)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK_TblWorkTimes_TblRestaurant");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
