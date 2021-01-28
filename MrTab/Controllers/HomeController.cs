using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace MrTab.Controllers
{
    public class HomeController : Controller
    {
        private Core db = new Core();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult Search(string name = null,string foodType=null, string nameFood = null, string CityInput = null,int orderBy=0)
        {
            ViewBag.name = name;
            ViewBag.CityInput = CityInput;
            List<TblRestaurant> list = db.Restaurant.Get().Where(i => i.IsValid).ToList();
            if (name != null)
            {
                list = list.Where(i => i.Name.Contains(name)).ToList();
            }
            if (nameFood != null)
            {
                List<TblRestaurant> food = db.Food.Get().Where(i => i.Name.Contains(nameFood)).Select(i => i.Restaurant).ToList();
                list.AddRange(food.Distinct());
            }
            if (foodType != null && foodType != "نوع غذا")
            {
                List<TblRestaurant> food = db.FoodType.Get().Where(i => i.Name.Contains(foodType)).Select(i => i.Restaurant).ToList();
                list.AddRange(food.Distinct());
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
                else if(orderBy == 2)
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
            return View(db.Config.Get().FirstOrDefault(i => i.Keyword.Contains("Gavanin")));
        }

        public IActionResult Rules()
        {
            return View(db.Config.Get().FirstOrDefault(i => i.Keyword.Contains("Gavanin")));
        }

    }
}
