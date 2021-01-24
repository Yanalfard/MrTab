using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DataLayer.Metadata
{
    public partial class MdReport
    {
        [Key]
        public int ReportId { get; set; }

        [Display(Name = "دلیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(200)]
        public string Reason { get; set; }
        
        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(500)]
        public string Description { get; set; }
        public int RestaurantId { get; set; }
    }


}
