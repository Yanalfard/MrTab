using System;
using System.Collections.Generic;

#nullable disable

namespace MrTab.Models
{
    public partial class TblProperty
    {
        public int PropertyId { get; set; }
        public string Name { get; set; }
        public int RestaurantId { get; set; }

        public virtual TblRestaurant Restaurant { get; set; }
    }
}
