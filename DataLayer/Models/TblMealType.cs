using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DataLayer.Models
{
    [Table("TblMealType")]
    public partial class TblMealType
    {
        [Key]
        public int MealTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public int RestaurantId { get; set; }

        [ForeignKey(nameof(RestaurantId))]
        [InverseProperty(nameof(TblRestaurant.TblMealTypes))]
        public virtual TblRestaurant Restaurant { get; set; }
    }
}
