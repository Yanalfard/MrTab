using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DataLayer.Models
{
    public partial class TblRestaurant
    {
        public TblRestaurant()
        {
            TblComment = new HashSet<TblComment>();
            TblFood = new HashSet<TblFood>();
            TblFoodType = new HashSet<TblFoodType>();
            TblImage = new HashSet<TblImage>();
            TblMealType = new HashSet<TblMealType>();
            TblProperty = new HashSet<TblProperty>();
            TblReport = new HashSet<TblReport>();
            TblWorkTime = new HashSet<TblWorkTime>();
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
        public bool IsValid { get; set; }

        [ForeignKey(nameof(CatagoryId))]
        [InverseProperty(nameof(TblCatagory.TblRestaurant))]
        public virtual TblCatagory Catagory { get; set; }
        [ForeignKey(nameof(CityId))]
        [InverseProperty(nameof(TblCity.TblRestaurant))]
        public virtual TblCity City { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(TblUser.TblRestaurant))]
        public virtual TblUser User { get; set; }
        [InverseProperty("Restaurant")]
        public virtual ICollection<TblComment> TblComment { get; set; }
        [InverseProperty("Restaurant")]
        public virtual ICollection<TblFood> TblFood { get; set; }
        [InverseProperty("Restaurant")]
        public virtual ICollection<TblFoodType> TblFoodType { get; set; }
        [InverseProperty("Restaurant")]
        public virtual ICollection<TblImage> TblImage { get; set; }
        [InverseProperty("Restaurant")]
        public virtual ICollection<TblMealType> TblMealType { get; set; }
        [InverseProperty("Restaurant")]
        public virtual ICollection<TblProperty> TblProperty { get; set; }
        [InverseProperty("Restaurant")]
        public virtual ICollection<TblReport> TblReport { get; set; }
        [InverseProperty("Restaurant")]
        public virtual ICollection<TblWorkTime> TblWorkTime { get; set; }
    }
}
