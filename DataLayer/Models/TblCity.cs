using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DataLayer.Models
{
    [Table("TblCity")]
    public partial class TblCity
    {
        public TblCity()
        {
            TblRestaurants = new HashSet<TblRestaurant>();
        }

        [Key]
        public int CityId { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [InverseProperty(nameof(TblRestaurant.City))]
        public virtual ICollection<TblRestaurant> TblRestaurants { get; set; }
    }
}
