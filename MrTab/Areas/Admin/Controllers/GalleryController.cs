using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using MrTab.Utilities;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrTab.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("author,admin")]
    public class GalleryController : Controller
    {
        private Core db = new Core();
        public IActionResult ImagesUser()
        {
            return View(db.Image.Get(i => i.IsValid == false && i.Status == 2));
        }
    }
}
