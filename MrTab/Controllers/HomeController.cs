using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace MrTab.Controllers
{
    public class HomeController : Controller
    {
        private Core db = new Core();
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
        public IActionResult Search(string name = null,string cat=null, string foodType = null, string nameFood = null, string CityInput = null, int orderBy = 0, string lat = null, string lon = null)
        {

            List<TblRestaurant> list = db.Restaurant.Get().Where(i => i.IsValid).ToList();
            if (lat != null && lon != null)
            {
                double convertlat = Convert.ToDouble(lat);
                double convertlon = Convert.ToDouble(lon);
                ///Lot
                double convertlatSmall = convertlat - 0.02;
                double convertlatBig = convertlat + 0.02;
                ///lon
                double convertlonSmall = convertlon - 0.02;
                double convertlonBig = convertlon + 0.02;
                list = list.Where(i => Convert.ToDouble(i.Lat) > convertlatSmall && Convert.ToDouble(i.Lat) < convertlatBig && Convert.ToDouble(i.Lon) > convertlonSmall && Convert.ToDouble(i.Lon) < convertlonBig).ToList();
            }
            if (name != null)
            {
                list = list.Where(i => i.Name.Contains(name)).ToList();
            }
            if (cat != null)
            {
                list = list.Where(i => i.Catagory.Name.Contains(cat)).ToList();
            }
            if (nameFood != null)
            {
                List<TblRestaurant> food = db.Food.Get().Where(i => i.Name.Contains(nameFood)).Select(i => i.Restaurant).ToList();
                foreach (var item in food)
                {
                    list = list.Where(i => i.RestaurantId == item.RestaurantId).ToList();
                }
            }
            if (foodType != null && foodType != "نوع غذا")
            {
                List<TblRestaurant> food = db.FoodType.Get().Where(i => i.Name.Contains(foodType)).Select(i => i.Restaurant).ToList();
                foreach (var item in food)
                {
                    list = list.Where(i => i.RestaurantId == item.RestaurantId).ToList();
                }
            }
            if (CityInput != null && CityInput != "شهر")
            {
                list = list.Where(i => i.City.Name.Contains(CityInput)).ToList();
            }
            if (orderBy > 0)
            {
                if (orderBy == 1)
                {
                    list.OrderByDescending(i => i.Rating);
                }
                else if (orderBy == 2)
                {
                    list.OrderBy(i => i.Rating);
                }
            }
            return View(list);
        }
        [Route("CategoryView/{id}/{name}")]
        public IActionResult CategoryView(int id, string name)
        {
            return View(db.Catagory.GetById(id));
        }
        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult ToRestaurant()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View(db.Config.Get().FirstOrDefault(i => i.Keyword.Contains("ContactUs")));
        }

        public IActionResult Rules()
        {
            return View(db.Config.Get().FirstOrDefault(i => i.Keyword.Contains("Gavanin")));
        }

    }
}
