using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;

namespace DataLayer.ViewModel
{
    public class ConfigVm
    {
        [Key]
        [StringLength(100)]
        public string Keyword { get; set; }
        [AllowHtml]
        public string Value { get; set; }
    }
}
