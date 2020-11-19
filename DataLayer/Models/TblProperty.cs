using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DataLayer.Models
{
    [Table("TblProperty")]
    public partial class TblProperty
    {
        [Key]
        public int PropertyId { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        public int RestaurantId { get; set; }

        [ForeignKey(nameof(RestaurantId))]
        [InverseProperty(nameof(TblRestaurant.TblProperties))]
        public virtual TblRestaurant Restaurant { get; set; }
    }
}
