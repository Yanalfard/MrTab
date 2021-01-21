using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DataLayer.Models1
{
    public partial class TblCatagory
    {
        public TblCatagory()
        {
            TblRestaurant = new HashSet<TblRestaurant>();
        }

        [Key]
        public int CatagoryId { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [StringLength(500)]
        public string ImageUrl { get; set; }

        [InverseProperty("Catagory")]
        public virtual ICollection<TblRestaurant> TblRestaurant { get; set; }
    }
}
