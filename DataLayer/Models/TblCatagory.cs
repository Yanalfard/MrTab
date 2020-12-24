﻿using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Models
{
    public partial class TblCatagory
    {
        public TblCatagory()
        {
            TblRestaurants = new HashSet<TblRestaurant>();
        }

        public int CatagoryId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public virtual ICollection<TblRestaurant> TblRestaurants { get; set; }
    }
}
