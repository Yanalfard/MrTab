﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MrTab.Controllers
{
    public class RestaurantController : Controller
    {
        public IActionResult ViewSingle()
        {
            return View();
        }



    }
}