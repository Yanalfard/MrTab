using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DataLayer.Models
{
    public partial class TblWorkTime
    {
        [Key]
        public int WorkTimeId { get; set; }
        [Required]
        [StringLength(100)]
        public string Day { get; set; }
        [Required]
        [StringLength(100)]
        public string Time { get; set; }
        public int RestaurantId { get; set; }

        [ForeignKey(nameof(RestaurantId))]
        [InverseProperty(nameof(TblRestaurant.TblWorkTimes))]
        public virtual TblRestaurant Restaurant { get; set; }
    }
}
