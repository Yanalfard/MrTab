using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DataLayer.Models
{
    [Table("TblRestaurant")]
    public partial class TblRestaurant
    {
        public TblRestaurant()
        {
            TblComments = new HashSet<TblComment>();
            TblFoodTypes = new HashSet<TblFoodType>();
            TblFoods = new HashSet<TblFood>();
            TblImages = new HashSet<TblImage>();
            TblMealTypes = new HashSet<TblMealType>();
            TblProperties = new HashSet<TblProperty>();
            TblReports = new HashSet<TblReport>();
            TblWorkTimes = new HashSet<TblWorkTime>();
        }

        [Key]
        public int RestaurantId { get; set; }
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
        [StringLength(500)]
        public string MainImage { get; set; }
        [StringLength(500)]
        public string MainBanner { get; set; }
        [StringLength(200)]
        public string ShortDesc { get; set; }
        [StringLength(1000)]
        public string LongDesc { get; set; }
        public int CatagoryId { get; set; }
        [StringLength(500)]
        public string Address { get; set; }
        public int CityId { get; set; }
        [StringLength(50)]
        public string Neighborhood { get; set; }
        [StringLength(50)]
        public string Lat { get; set; }
        [StringLength(50)]
        public string Lon { get; set; }
        [StringLength(20)]
        public string TellNo1 { get; set; }
        [StringLength(20)]
        public string TellNo2 { get; set; }
        public short Rating { get; set; }
        public int RateCount { get; set; }
        public short ExpenseRate { get; set; }
        public short QualityRate { get; set; }
        public short ServiceRate { get; set; }
        public short DecorRate { get; set; }
        public short QualityPerPriceRate { get; set; }
        [StringLength(1000)]
        public string InstagramUrl { get; set; }
        public int UserId { get; set; }

        [ForeignKey(nameof(CatagoryId))]
        [InverseProperty(nameof(TblCatagory.TblRestaurants))]
        public virtual TblCatagory Catagory { get; set; }
        [ForeignKey(nameof(CityId))]
        [InverseProperty(nameof(TblCity.TblRestaurants))]
        public virtual TblCity City { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(TblUser.TblRestaurants))]
        public virtual TblUser User { get; set; }
        [InverseProperty(nameof(TblComment.Restaurant))]
        public virtual ICollection<TblComment> TblComments { get; set; }
        [InverseProperty(nameof(TblFoodType.Restaurant))]
        public virtual ICollection<TblFoodType> TblFoodTypes { get; set; }
        [InverseProperty(nameof(TblFood.Restaurant))]
        public virtual ICollection<TblFood> TblFoods { get; set; }
        [InverseProperty(nameof(TblImage.Restaurant))]
        public virtual ICollection<TblImage> TblImages { get; set; }
        [InverseProperty(nameof(TblMealType.Restaurant))]
        public virtual ICollection<TblMealType> TblMealTypes { get; set; }
        [InverseProperty(nameof(TblProperty.Restaurant))]
        public virtual ICollection<TblProperty> TblProperties { get; set; }
        [InverseProperty(nameof(TblReport.Restaurant))]
        public virtual ICollection<TblReport> TblReports { get; set; }
        [InverseProperty(nameof(TblWorkTime.Restaurant))]
        public virtual ICollection<TblWorkTime> TblWorkTimes { get; set; }
    }
}
