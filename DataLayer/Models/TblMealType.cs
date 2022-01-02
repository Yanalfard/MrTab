using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DataLayer.Models
{
    [Table("TblMealType", Schema = "dbo")]
    public partial class TblMealType
    {
        [Key]
        public int MealTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public int RestaurantId { get; set; }

        [ForeignKey(nameof(RestaurantId))]
        [InverseProperty(nameof(TblRestaurant.TblMealType))]
        public virtual TblRestaurant Restaurant { get; set; }
    }
}
