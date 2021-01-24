using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DataLayer.Metadata
{
    public partial class MdComment
    {
        [Key]
        public int CommentId { get; set; }
        [Display(Name = "نظر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(500)]
        public string Text { get; set; }
        [Display(Name = "تعداد لایک")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int LikeCount { get; set; }
        [Display(Name = "گزارش شده؟")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public bool IsReported { get; set; }
        [Display(Name = "امتیاز")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public short Rate { get; set; }
        [Display(Name = "امتیاز قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public short ExpenseRate { get; set; }
        [Display(Name = "امتیاز کیفیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public short QualityRate { get; set; }
        [Display(Name = "امتیاز سرویس دهی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public short ServiceRate { get; set; }
        [Display(Name = "امیتاز دکور و زیبایی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public short DecorRate { get; set; }
        [Display(Name = "امتیاز قیمت نسبت به کیفیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public short QualityPerPriceRate { get; set; }
        public int AnswerId { get; set; }
        
        [Display(Name = "تاریخ ثبت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Column(TypeName = "datetime")]
        public DateTime DateSubmited { get; set; }
        public int RestaurantId { get; set; }
        public int UserId { get; set; }
    }


}
