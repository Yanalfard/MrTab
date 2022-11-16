using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace DataLayer.ViewModel
{
    public class HomeImageTextVm
    {
        public string MainBanner { get; set; }
        public string MainColor { get; set; }
       
        public string MainImage { get; set; }
        [AllowHtml]
        public string MainText { get; set; }
        public string MobileAppBackGroundImage { get; set; }
        [AllowHtml]
        public string MobileAppBackGroundText { get; set; }
        public string UkLiColor { get; set; }
        public string LocationSearchTextSlider { get; set; }
    }
}
