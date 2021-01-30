using DataLayer.Metadata;
using DataLayer.Models;
using DataLayer.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MrTab.Utilities;
using Services.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MrTab.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("admin")]
    public class AboutContactController : Controller
    {
        private Core db = new Core();
        public IActionResult About()
        {
            TblConfig updateConfig1 = db.Config.Get().FirstOrDefault(i => i.Keyword.Contains("AboutPar1"));
            TblConfig updateConfig2 = db.Config.Get().FirstOrDefault(i => i.Keyword.Contains("AboutPar2"));
            TblConfig updateConfig3 = db.Config.Get().FirstOrDefault(i => i.Keyword.Contains("AboutPar3"));
            AboutUsVm aboutVm = new AboutUsVm();
            aboutVm.AboutPar1 = updateConfig1.Value;
            aboutVm.AboutPar2 = updateConfig2.Value;
            aboutVm.AboutPar3 = updateConfig3.Value;
            return View(aboutVm);
        }
        [HttpPost]
        public IActionResult About(AboutUsVm aboutUs)
        {
            TblConfig updateConfig1 = db.Config.Get().FirstOrDefault(i => i.Keyword.Contains("AboutPar1"));
            TblConfig updateConfig2 = db.Config.Get().FirstOrDefault(i => i.Keyword.Contains("AboutPar2"));
            TblConfig updateConfig3 = db.Config.Get().FirstOrDefault(i => i.Keyword.Contains("AboutPar3"));
            if (updateConfig1 != null)
            {
                updateConfig1.Value = aboutUs.AboutPar1;
            }
            if (updateConfig2 != null)
            {
                updateConfig2.Value = aboutUs.AboutPar2;
            }
            if (updateConfig3 != null)
            {
                updateConfig3.Value = aboutUs.AboutPar3;
            }
            db.Config.Save();
            return View(aboutUs);
        }
        public IActionResult ContactUs()
        {
            TblConfig updateConfig = db.Config.Get().FirstOrDefault(i => i.Keyword.Contains("ContactUs"));
            ConfigVm selectedConfig = new ConfigVm();
            selectedConfig.Keyword = updateConfig.Keyword;
            selectedConfig.Value = updateConfig.Value;
            return View(selectedConfig);
        }
        [HttpPost]
        public IActionResult ContactUs(ConfigVm config)
        {
            TblConfig updateConfig = db.Config.Get().FirstOrDefault(i => i.Keyword.Contains("ContactUs"));
            if (updateConfig != null)
            {
                updateConfig.Value = config.Value;
                db.Config.Save();
            }
            return View(config);
        }

        public IActionResult Rules()
        {
            TblConfig updateConfig = db.Config.Get().FirstOrDefault(i => i.Keyword.Contains("Gavanin"));
            ConfigVm selectedConfig = new ConfigVm();
            selectedConfig.Keyword = updateConfig.Keyword;
            selectedConfig.Value = updateConfig.Value;
            return View(selectedConfig);
        }
        [HttpPost]
        public IActionResult Rules(ConfigVm config)
        {
            TblConfig updateConfig = db.Config.Get().FirstOrDefault(i => i.Keyword.Contains("Gavanin"));
            if (updateConfig != null)
            {
                updateConfig.Value = config.Value;
                db.Config.Save();
            }
            return View(config);
        }

        public IActionResult Index()
        {
            TblConfig updateConfig1 = db.Config.Get().FirstOrDefault(i => i.Keyword.Contains("MainBanner"));
            TblConfig updateConfig2 = db.Config.Get().FirstOrDefault(i => i.Keyword.Contains("MainColor"));
            TblConfig updateConfig3 = db.Config.Get().FirstOrDefault(i => i.Keyword.Contains("MainImage"));
            TblConfig updateConfig4 = db.Config.Get().FirstOrDefault(i => i.Keyword.Contains("MainText"));
            TblConfig updateConfig5 = db.Config.Get().FirstOrDefault(i => i.Keyword.Contains("MobileAppBackGroundImage"));
            TblConfig updateConfig6 = db.Config.Get().FirstOrDefault(i => i.Keyword.Contains("MobileAppBackGroundText"));
            HomeImageTextVm homeVm = new HomeImageTextVm();
            homeVm.MainBanner = updateConfig1.Value;
            homeVm.MainColor = updateConfig2.Value;
            homeVm.MainImage = updateConfig3.Value;
            homeVm.MainText = updateConfig4.Value;
            homeVm.MobileAppBackGroundImage = updateConfig5.Value;
            homeVm.MobileAppBackGroundText = updateConfig6.Value;
            return View(homeVm);
        }
        [HttpPost]
        public async Task<IActionResult> Index(HomeImageTextVm homeVm, IFormFile MainBanner, IFormFile MainImage, IFormFile MobileApp)
        {
            TblConfig updateConfig1 = db.Config.Get().FirstOrDefault(i => i.Keyword.Contains("MainBanner"));
            TblConfig updateConfig2 = db.Config.Get().FirstOrDefault(i => i.Keyword.Contains("MainColor"));
            TblConfig updateConfig3 = db.Config.Get().FirstOrDefault(i => i.Keyword.Contains("MainImage"));
            TblConfig updateConfig4 = db.Config.Get().FirstOrDefault(i => i.Keyword.Contains("MainText"));
            TblConfig updateConfig5 = db.Config.Get().FirstOrDefault(i => i.Keyword.Contains("MobileAppBackGroundImage"));
            TblConfig updateConfig6 = db.Config.Get().FirstOrDefault(i => i.Keyword.Contains("MobileAppBackGroundText"));
            if (updateConfig1 != null)
            {
                if (MainBanner != null)
                {
                    if (updateConfig1.Value != null)
                    {
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Home/Baner/", homeVm.MainBanner);
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    homeVm.MainBanner = Guid.NewGuid().ToString() + Path.GetExtension(MainBanner.FileName);
                    string savePath = Path.Combine(
                       Directory.GetCurrentDirectory(), "wwwroot/Images/Home/Baner/", homeVm.MainBanner
                   );
                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                       await MainBanner.CopyToAsync(stream);
                    }
                }
                updateConfig1.Value = homeVm.MainBanner;
            }
            if (updateConfig2 != null)
            {
                updateConfig2.Value = homeVm.MainColor;
            }
            if (updateConfig3 != null)
            {
                if (MainImage != null)
                {
                    if (updateConfig3.Value != null)
                    {
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Home/Image/", homeVm.MainImage);
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    homeVm.MainImage = Guid.NewGuid().ToString() + Path.GetExtension(MainImage.FileName);
                    string savePath = Path.Combine(
                       Directory.GetCurrentDirectory(), "wwwroot/Images/Home/Image/", homeVm.MainImage
                   );
                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await MainImage.CopyToAsync(stream);
                    }
                }
                updateConfig3.Value = homeVm.MainImage;
            }
            if (updateConfig4 != null)
            {
                updateConfig4.Value = homeVm.MainText;
            }
            if (updateConfig5 != null)
            {
                if (MobileApp != null)
                {
                    if (updateConfig5.Value != null)
                    {
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Home/MobileApp/", homeVm.MobileAppBackGroundImage);
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    homeVm.MobileAppBackGroundImage = Guid.NewGuid().ToString() + Path.GetExtension(MobileApp.FileName);
                    string savePath = Path.Combine(
                       Directory.GetCurrentDirectory(), "wwwroot/Images/Home/MobileApp/", homeVm.MobileAppBackGroundImage
                   );
                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await MobileApp.CopyToAsync(stream);
                    }
                }
                updateConfig5.Value = homeVm.MobileAppBackGroundImage;
            }

            if (updateConfig6 != null)
            {
                updateConfig6.Value = homeVm.MobileAppBackGroundText;
            }
            db.Config.Save();
            return View(homeVm);
        }
    }
}
