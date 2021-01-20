using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DataLayer.Metadata
{
    public partial class MdWorkTime
    {
        [Key]
        public int WorkTimeId { get; set; }
        [Display(Name = "روز")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(100)]
        public string Day { get; set; }
        [Display(Name = "زمان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(100)]
        public string Time { get; set; }
        public int RestaurantId { get; set; }
    }


}
