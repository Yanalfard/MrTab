using DataLayer.Metadata;
using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrTab.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RestauranController : Controller
    {
        private Core db = new Core();
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.CityId = db.City.Get();
            ViewBag.UserId = db.User.Get().Where(i => i.RoleId != 3);
            ViewBag.CatagoryId = db.Catagory.Get();
            return await Task.FromResult(View());
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(MdRestaurant restaurant, IFormFile MainImage, IFormFile MainBanner)
        {
            ViewBag.CityId = db.City.Get();
            ViewBag.UserId = db.User.Get().Where(i => i.RoleId != 3);
            ViewBag.CatagoryId = db.Catagory.Get();
            if (ModelState.IsValid)
            {
                TblRestaurant addRestaurant = new TblRestaurant();
                addRestaurant.Address = restaurant.Address;
                addRestaurant.CatagoryId = restaurant.CatagoryId;
                addRestaurant.CityId = restaurant.CityId;
                addRestaurant.InstagramUrl = restaurant.InstagramUrl;
                addRestaurant.Lat = restaurant.Lat;
                addRestaurant.Lon = restaurant.Lon;
                addRestaurant.LongDesc = restaurant.LongDesc;
                addRestaurant.MainBanner = restaurant.MainBanner;
                addRestaurant.MainImage = restaurant.MainImage;
                addRestaurant.Name = restaurant.Name;
                addRestaurant.Neighborhood = restaurant.Neighborhood;
                addRestaurant.ShortDesc = restaurant.ShortDesc;
                addRestaurant.TellNo1 = restaurant.TellNo1;
                addRestaurant.TellNo2 = restaurant.TellNo2;
                addRestaurant.UserId = restaurant.UserId;


                return await Task.FromResult(RedirectToAction(nameof(Index)));
            }
            return await Task.FromResult(View(restaurant));
        }

    }
}
