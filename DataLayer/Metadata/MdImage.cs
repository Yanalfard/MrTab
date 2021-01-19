using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DataLayer.Metadata
{
    public partial class MdImage
    {
        [Key]
        public int ImageId { get; set; }
        [Display(Name = "لینک")]
        [StringLength(1000)]
        public string Url { get; set; }
        [Display(Name = "عنوان عکس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Status { get; set; }
        public int RestaurantId { get; set; }
        [Display(Name = "تایید شده؟")]
        public bool IsValid { get; set; }

    }


}
