using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DataLayer.Models1
{
    public partial class TblCity
    {
        public TblCity()
        {
            TblRestaurant = new HashSet<TblRestaurant>();
        }

        [Key]
        public int CityId { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [InverseProperty("City")]
        public virtual ICollection<TblRestaurant> TblRestaurant { get; set; }
    }
}
