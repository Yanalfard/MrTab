using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DataLayer.Metadata
{
    public partial class MdUser
    {
        [Key]
        public int UserId { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(150)]
        public string Name { get; set; }
        [Display(Name = "تلفن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(13)]
        public string TellNo { get; set; }
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(64)]
        public string Password { get; set; }
        [Display(Name = "نقش")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
        [StringLength(256)]
        public string Auth { get; set; }
        [Display(Name = "عکس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(500)]
        public string ImageUrl { get; set; }
    }


}
