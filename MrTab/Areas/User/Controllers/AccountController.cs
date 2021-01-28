using DataLayer.Models;
using DataLayer.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MrTab.Utilities;
using Services.Security;
using Services.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MrTab.Areas.User.Controllers
{
    [Area("User")]
    [PermissionChecker("user")]
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private Core db = new Core();
        TblUser SelectUser()
        {
            int userId = Convert.ToInt32(User.Claims.First().Value);
            TblUser selectUser = db.User.GetById(userId);
            return selectUser;
        }
        public async Task<string> Information()
        {
            return await Task.FromResult(SelectUser().Name);
        }
        public async Task<IActionResult> ChangePassword()
        {
            return await Task.FromResult(ViewComponent("ChangePasswordInUserVm"));
        }
        [HttpPost]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordUserVm change)
        {
            if (ModelState.IsValid)
            {
                TblUser updateUser = db.User.GetById(SelectUser().UserId);
                updateUser.Password = PasswordHelper.EncodePasswordMd5(change.Password);
                db.User.Update(updateUser);
                db.User.Save();
                return await Task.FromResult(PartialView("ChangePassword", change));
            }
            return await Task.FromResult(PartialView("ChangePassword", change));
        }

        public async Task<IActionResult> EditName()
        {
            return await Task.FromResult(ViewComponent("EditNameInUserVm", new { name = SelectUser().Name }));
        }
        [HttpPost]
        public async Task<IActionResult> EditNameAsync(EditNameUserVm nameUser)
        {
            if (ModelState.IsValid)
            {
                TblUser updateUser = db.User.GetById(SelectUser().UserId);
                updateUser.Name = nameUser.Name;
                db.User.Update(updateUser);
                db.User.Save();
                return await Task.FromResult(PartialView("EditName", nameUser));
            }
            return await Task.FromResult(PartialView("EditName", nameUser));
        }




        public async Task<IActionResult> UploadImage()
        {
            return await Task.FromResult(ViewComponent("UploadImageInUserVm"));
        }
        [HttpPost]
        public async Task<IActionResult> UploadImageAsync(UploadImageRestaurant uploadImage, IFormFile files)
        {
            if (files != null)
            {
                TblUser selectedUser = db.User.GetById(SelectUser().UserId);
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Users/", selectedUser.ImageUrl);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                uploadImage.Image = Guid.NewGuid().ToString() + Path.GetExtension(files.FileName);
                string savePath = Path.Combine(
                    Directory.GetCurrentDirectory(), "wwwroot/Images/Users/", uploadImage.Image
                );
                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await files.CopyToAsync(stream);
                }
                selectedUser.ImageUrl = uploadImage.Image;
                db.User.Save();
                return await Task.FromResult(Redirect("/User/User/Index"));
            }
            return await Task.FromResult(Redirect("/User/User/Index"));
        }
    }
}
