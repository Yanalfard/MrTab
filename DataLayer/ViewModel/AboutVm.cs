using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace DataLayer.ViewModel
{
    public class AboutVm
    {
        [AllowHtml]
        public string AboutPar1 { get; set; }
        [AllowHtml]
        public string AboutPar2 { get; set; }
        [AllowHtml]
        public string AboutPar3 { get; set; }
    }
}
