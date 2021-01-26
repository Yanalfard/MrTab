using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MrTab.Utilities;
using Services.Services;

namespace MrTab.Areas.User.Controllers
{
    [Area("User")]
    [PermissionChecker("user")]
    public class UserController : Controller
    {
        private Core db = new Core();
        TblUser SelectUser()
        {
            int userId = Convert.ToInt32(User.Claims.First().Value);
            TblUser selectUser = db.User.GetById(userId);
            return selectUser;
        }
        public IActionResult Index()
        {
            ViewBag.Id = SelectUser().UserId;
            return View();
        }    
        public IActionResult ToRestaurant()
        {
            return View();
        }

        public async Task<IActionResult> ChangePassword()
        {
            int id = Convert.ToInt32(User.Claims.First().Value);
            return await Task.FromResult(ViewComponent("ChangePasswordInUserVm", new { id = id }));
        }

        public IActionResult Menu()
        {
            return View();
        }

    }
}
