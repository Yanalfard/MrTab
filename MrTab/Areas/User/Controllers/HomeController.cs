using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MrTab.Utilities;

namespace MrTab.Areas.User.Controllers
{
    [Area("User")]
    [WebAuthorize("user")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
