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
        public IActionResult Search(string name="",string address="",string city="")
        {
            ViewBag.name = name;
            ViewBag.address = address;
            ViewBag.city = city;
            List<TblRestaurant> list = db.Restaurant.Get().ToList();
            if (name != "")
            {
                list = list.Where(i => i.Name.Contains(name)).ToList();
            }
            if (address != "")
            {
                list = list.Where(i => i.Address.Contains(address)).ToList();
            }
            if (name != "")
            {
                list = list.Where(i => i.City.Name.Contains(city)).ToList();
            }
            return View(list);
        }
        [Route("CategoryView/{id}/{name}")]
        public IActionResult CategoryView(int id,string name)
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
    }
}
