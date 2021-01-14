using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.ViewModel
{
    public class UserVm
    {
        [Key]
        public int UserId { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(150)]
        public string Name { get; set; }
        [Display(Name = "تلفن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(11)]
        [MinLength(11,ErrorMessage ="شماره کمتر از 11 کاراکتر است")]
        [RegularExpression("[0]{1}[9]{1}[0-9]{9}", ErrorMessage = "شماره تلفن وارد شده معتبر نمی باشد")]
        [DataType(DataType.PhoneNumber,ErrorMessage ="شماره نامعتبر است")]
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
        [StringLength(500)]
        public string ImageUrl { get; set; }
    }
    public class EditUserVm
    {
        [Key]
        public int UserId { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(150)]
        public string Name { get; set; }
        [Display(Name = "تلفن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(11)]
        [MinLength(11, ErrorMessage = "شماره کمتر از 11 کاراکتر است")]
        [RegularExpression("[0]{1}[9]{1}[0-9]{9}", ErrorMessage = "شماره تلفن وارد شده معتبر نمی باشد")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "شماره نامعتبر است")]
        public string TellNo { get; set; }
        [Display(Name = "نقش")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
        [StringLength(256)]
        public string Auth { get; set; }
        [Display(Name = "عکس")]
        [StringLength(500)]
        public string ImageUrl { get; set; }
    }
}
