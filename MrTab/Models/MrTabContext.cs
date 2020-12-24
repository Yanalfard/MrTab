using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MrTab.Models
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
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=103.216.62.27;Initial Catalog=MrTab;User ID=Yanal;Password=1710ahmad.fard");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblCatagory>(entity =>
            {
                entity.HasKey(e => e.CatagoryId);

                entity.ToTable("TblCatagory");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<TblCity>(entity =>
            {
                entity.HasKey(e => e.CityId);

                entity.ToTable("TblCity");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<TblComment>(entity =>
            {
                entity.HasKey(e => e.CommentId);

                entity.ToTable("TblComment");

                entity.Property(e => e.DateSubmited).HasColumnType("datetime");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(500);

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

            modelBuilder.Entity<TblConfig>(entity =>
            {
                entity.HasKey(e => e.Keyword);

                entity.ToTable("TblConfig");

                entity.Property(e => e.Keyword).HasMaxLength(100);

                entity.Property(e => e.Value).HasMaxLength(1000);
            });

            modelBuilder.Entity<TblFood>(entity =>
            {
                entity.HasKey(e => e.FoodId);

                entity.ToTable("TblFood");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.TblFoods)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK_TblFood_TblRestaurant");
            });

            modelBuilder.Entity<TblFoodType>(entity =>
            {
                entity.HasKey(e => e.FoodTypeId);

                entity.ToTable("TblFoodType");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.TblFoodTypes)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK_TblFoodType_TblRestaurant");
            });

            modelBuilder.Entity<TblImage>(entity =>
            {
                entity.HasKey(e => e.ImageId);

                entity.ToTable("TblImage");

                entity.Property(e => e.Status).HasComment("0 is Normal; 1 is Menu");

                entity.Property(e => e.Url).HasMaxLength(1000);

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.TblImages)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK_TblImage_TblRestaurant");
            });

            modelBuilder.Entity<TblMealType>(entity =>
            {
                entity.HasKey(e => e.MealTypeId);

                entity.ToTable("TblMealType");

                entity.Property(e => e.MealTypeId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.TblMealTypes)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK_TblMealType_TblRestaurant");
            });

            modelBuilder.Entity<TblProperty>(entity =>
            {
                entity.HasKey(e => e.PropertyId);

                entity.ToTable("TblProperty");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.TblProperties)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK_TblProperty_TblRestaurant");
            });

            modelBuilder.Entity<TblReport>(entity =>
            {
                entity.HasKey(e => e.ReportId);

                entity.ToTable("TblReport");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Reason)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.TblReports)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK_TblReport_TblRestaurant");
            });

            modelBuilder.Entity<TblRestaurant>(entity =>
            {
                entity.HasKey(e => e.RestaurantId);

                entity.ToTable("TblRestaurant");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.InstagramUrl).HasMaxLength(1000);

                entity.Property(e => e.Lat).HasMaxLength(50);

                entity.Property(e => e.Lon).HasMaxLength(50);

                entity.Property(e => e.LongDesc).HasMaxLength(1000);

                entity.Property(e => e.MainBanner).HasMaxLength(500);

                entity.Property(e => e.MainImage).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Neighborhood).HasMaxLength(50);

                entity.Property(e => e.ShortDesc).HasMaxLength(200);

                entity.Property(e => e.TellNo1).HasMaxLength(20);

                entity.Property(e => e.TellNo2).HasMaxLength(20);

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
                entity.HasKey(e => e.RoleId);

                entity.ToTable("TblRole");

                entity.Property(e => e.RoleId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("TblUser");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.Auth).HasMaxLength(256);

                entity.Property(e => e.ImageUrl).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.TellNo)
                    .IsRequired()
                    .HasMaxLength(13);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TblUsers)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TblUser_TblRole");
            });

            modelBuilder.Entity<TblWorkTime>(entity =>
            {
                entity.HasKey(e => e.WorkTimeId);

                entity.Property(e => e.Day)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Time)
                    .IsRequired()
                    .HasMaxLength(100);

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
