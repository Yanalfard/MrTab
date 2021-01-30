using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;

namespace DataLayer.ViewModel
{
    public class DocVm
    {
        [Key]
        public int DocId { get; set; }
        [StringLength(500)]
        public string VideoUrl { get; set; }
        [StringLength(4000)]
        [Display(Name = "توضیحات ")]
        [AllowHtml]
        public string BodyHtml { get; set; }
        [StringLength(500)]
        public string MainImage1 { get; set; }
        [StringLength(500)]
        public string MainImage2 { get; set; }
        [StringLength(500)]
        public string MainImage3 { get; set; }
        [StringLength(500)]
        [Display(Name = "نام ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }
        public int LikeCount { get; set; }
    }
}
