using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DataLayer.Models
{
    [Table("TblCatagory")]
    public partial class TblCatagory
    {
        public TblCatagory()
        {
            TblRestaurants = new HashSet<TblRestaurant>();
        }

        [Key]
        public int CatagoryId { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [StringLength(500)]
        public string ImageUrl { get; set; }

        [InverseProperty(nameof(TblRestaurant.Catagory))]
        public virtual ICollection<TblRestaurant> TblRestaurants { get; set; }
    }
}
