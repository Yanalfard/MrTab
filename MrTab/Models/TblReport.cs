using System;
using System.Collections.Generic;

#nullable disable

namespace MrTab.Models
{
    public partial class TblReport
    {
        public int ReportId { get; set; }
        public string Reason { get; set; }
        public string Description { get; set; }
        public int RestaurantId { get; set; }

        public virtual TblRestaurant Restaurant { get; set; }
    }
}
