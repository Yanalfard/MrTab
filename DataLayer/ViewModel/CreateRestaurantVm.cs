using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.ViewModel
{
    public class CreateRestaurantVm
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
        [StringLength(200)]
        [DataType(DataType.MultilineText)]
        public string ShortDesc { get; set; }
        [Display(Name = "توضیحات")]
        [StringLength(1000)]
        [DataType(DataType.MultilineText)]
        public string LongDesc { get; set; }
        [Display(Name = "دسته بندی ")]
        public int CatagoryId { get; set; } = 0;
        [Display(Name = "آدرس")]
        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        [Display(Name = " شهر ")]
        public int CityId { get; set; } = 0;
        [Display(Name = "محله")]
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
        [StringLength(20)]
        public string TellNo1 { get; set; }
        [Display(Name = "تلفن 2")]
        [StringLength(20)]
        public string TellNo2 { get; set; }
        [Display(Name = "امتیاز")]
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
        [StringLength(1000)]
        public string InstagramUrl { get; set; }
        [Display(Name = "کاربر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int UserId { get; set; }
        public bool IsValid { get; set; }
        [Display(Name = "نام شهر ")]
        [StringLength(200)]
        public string NameCity { get; set; }
        [Display(Name = "نام گروه ")]
        [StringLength(200)]
        public string NameCatagory{ get; set; }
        [Display(Name = "نام غذا")]
        [StringLength(200)]
        public string NameFood { get; set; }
        [Display(Name = "نوع رستوران")]
        [StringLength(200)]
        public string NameFoodType { get; set; }
        [Display(Name = "سبک غذاها")]
        [StringLength(200)]
        public string NameMealType { get; set; }
        [Display(Name = "امکانات")]
        [StringLength(200)]
        public string NameProperty { get; set; }
        [Display(Name = "ساعات کاری")]
        [StringLength(200)]
        public string NameWorkTime { get; set; }
        [Display(Name = "زمان کاری")]
        [StringLength(200)]
        public string NameWorkDay { get; set; }
    }
}
