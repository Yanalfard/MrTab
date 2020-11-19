﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DataLayer.Models
{
    [Table("TblFood")]
    public partial class TblFood
    {
        [Key]
        public int FoodId { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        public int RestaurantId { get; set; }

        [ForeignKey(nameof(RestaurantId))]
        [InverseProperty(nameof(TblRestaurant.TblFoods))]
        public virtual TblRestaurant Restaurant { get; set; }
    }
}