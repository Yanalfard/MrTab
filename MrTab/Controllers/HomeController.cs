﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MrTab.Controllers
{
    public class HomeController : Controller
    {
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
        public IActionResult CategoryView()
        {
            return View();
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
