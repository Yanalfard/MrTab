using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MrTab.Utilities;

namespace MrTab.Areas.User.Controllers
{
    [Area("User")]
    [PermissionChecker("user")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.UserId= Convert.ToInt32(User.Claims.First().Value);
            return View();
        }
        
    }
}
