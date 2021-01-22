using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MrTab.Utilities;

namespace MrTab.Areas.User.Controllers
{
    [Area("User")]
    //[Authorize]
    [PermissionChecker("user")]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ToRestaurant()
        {
            return View();
        }
    }
}
