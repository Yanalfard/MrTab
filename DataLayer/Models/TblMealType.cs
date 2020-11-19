using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
