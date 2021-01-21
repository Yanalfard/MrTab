using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.ViewModel
{
    public class RegisterVm
    {
        public int id { get; set; }
        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MinLength(11, ErrorMessage = "تعداد کاراکتر کم است")]
        [MaxLength(11, ErrorMessage = "تعداد کاراکتر بیشتر است")]
        [DataType(DataType.PhoneNumber)]
        //[RegularExpression("09(1[0-9]|3[1-9]|2[1-9])-?[0-9]{3}-?[0-9]{4}", ErrorMessage = "{0} معتبر نیست")]
        [RegularExpression("[0]{1}[9]{1}[0-9]{9}", ErrorMessage = "شماره تلفن وارد شده معتبر نمی باشد")]
        //[CodeMelli("لطفا کد ملی را بدرستی وارد کنید")]
        [StringLength(11)]
        public string TellNo { get; set; }
        [Display(Name = "کد واژه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MinLength(4, ErrorMessage = "تعداد کاراکتر کم است")]
        [StringLength(25)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "تکرار کد واژه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MinLength(4, ErrorMessage = "تعداد کاراکتر کم است")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "کلمه های عبور مغایرت دارند")]
        [StringLength(25)]
        public string RePassword { get; set; }
    }
}
