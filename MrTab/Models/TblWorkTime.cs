using System;
using System.Collections.Generic;

#nullable disable

namespace MrTab.Models
{
    public partial class TblWorkTime
    {
        public int WorkTimeId { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
        public int RestaurantId { get; set; }

        public virtual TblRestaurant Restaurant { get; set; }
    }
}
