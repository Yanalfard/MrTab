using System;
using System.Collections.Generic;

#nullable disable

namespace MrTab.Models
{
    public partial class TblImage
    {
        public int ImageId { get; set; }
        public string Url { get; set; }
        public int Status { get; set; }
        public int RestaurantId { get; set; }
        public bool IsValid { get; set; }

        public virtual TblRestaurant Restaurant { get; set; }
    }
}
