using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public IActionResult Search()
        {
            return View();
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
