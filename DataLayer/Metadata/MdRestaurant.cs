using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DataLayer.Metadata
{
    public partial class MdRestaurant
    {
        [Key]
        public int RestaurantId { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(128)]
        public string Name { get; set; }
        [Display(Name = "عکس اصلی")]
        [StringLength(500)]
        public string MainImage { get; set; }
        [Display(Name = "بنر اصلی")]
        [StringLength(500)]
        public string MainBanner { get; set; }
        [Display(Name = "توضیحات مختصر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(200)]
        [DataType(DataType.MultilineText)]
        public string ShortDesc { get; set; }
        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(1000)]
        [DataType(DataType.MultilineText)]
        public string LongDesc { get; set; }
        [Display(Name = "دسته بندی ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CatagoryId { get; set; }
        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        [Display(Name = " شهر ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CityId { get; set; }
        [Display(Name = "محله")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50)]
        public string Neighborhood { get; set; }
        [Display(Name = "طول جغرافیایی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50)]
        public string Lat { get; set; }
        [Display(Name = "عرض جغرافیایی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50)]
        public string Lon { get; set; }
        [Display(Name = "تلفن 1")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(20)]
        public string TellNo1 { get; set; }
        [Display(Name = "تلفن 2")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(20)]
        public string TellNo2 { get; set; }
        [Display(Name = "امتیاز")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public short Rating { get; set; }
        [Display(Name = "تعداد امتیاز")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int RateCount { get; set; }
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
        [Display(Name = "لیبنک اینستاگرام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(1000)]
        public string InstagramUrl { get; set; }
        [Display(Name = "کاربر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int UserId { get; set; }
    }

}
