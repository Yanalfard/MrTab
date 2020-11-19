using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DataLayer.Models
{
    [Table("TblFoodType")]
    public partial class TblFoodType
    {
        [Key]
        public int FoodTypeId { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        public int RestaurantId { get; set; }

        [ForeignKey(nameof(RestaurantId))]
        [InverseProperty(nameof(TblRestaurant.TblFoodTypes))]
        public virtual TblRestaurant Restaurant { get; set; }
    }
}
