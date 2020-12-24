using System;
using System.Collections.Generic;
using DataLayer.Models;

#nullable disable

namespace MrTab.Models
{
    public partial class TblCatagory
    {
        public TblCatagory()
        {
            TblRestaurants = new HashSet<TblRestaurant>();
        }

        public int CatagoryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TblRestaurant> TblRestaurants { get; set; }
    }
}
