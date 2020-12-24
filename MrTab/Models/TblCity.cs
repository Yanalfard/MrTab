using System;
using System.Collections.Generic;

#nullable disable

namespace MrTab.Models
{
    public partial class TblCity
    {
        public TblCity()
        {
            TblRestaurants = new HashSet<TblRestaurant>();
        }

        public int CityId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TblRestaurant> TblRestaurants { get; set; }
    }
}
