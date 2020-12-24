using System;
using System.Collections.Generic;

#nullable disable

namespace MrTab.Models
{
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

        public int RestaurantId { get; set; }
        public string Name { get; set; }
        public string MainImage { get; set; }
        public string MainBanner { get; set; }
        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }
        public int CatagoryId { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public string Neighborhood { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string TellNo1 { get; set; }
        public string TellNo2 { get; set; }
        public short Rating { get; set; }
        public int RateCount { get; set; }
        public short ExpenseRate { get; set; }
        public short QualityRate { get; set; }
        public short ServiceRate { get; set; }
        public short DecorRate { get; set; }
        public short QualityPerPriceRate { get; set; }
        public string InstagramUrl { get; set; }
        public int UserId { get; set; }

        public virtual TblCatagory Catagory { get; set; }
        public virtual TblCity City { get; set; }
        public virtual TblUser User { get; set; }
        public virtual ICollection<TblComment> TblComments { get; set; }
        public virtual ICollection<TblFoodType> TblFoodTypes { get; set; }
        public virtual ICollection<TblFood> TblFoods { get; set; }
        public virtual ICollection<TblImage> TblImages { get; set; }
        public virtual ICollection<TblMealType> TblMealTypes { get; set; }
        public virtual ICollection<TblProperty> TblProperties { get; set; }
        public virtual ICollection<TblReport> TblReports { get; set; }
        public virtual ICollection<TblWorkTime> TblWorkTimes { get; set; }
    }
}
