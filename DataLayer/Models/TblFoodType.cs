using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Models
{
    public partial class TblFoodType
    {
        public int FoodTypeId { get; set; }
        public string Name { get; set; }
        public int RestaurantId { get; set; }

        public virtual TblRestaurant Restaurant { get; set; }
    }
}
