using DataLayer.Models;
using DataLayer.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Security;
using Services.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MrTab.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private Core db = new Core();
        public ActionResult Index(string name = null, string tellNo = null, int isActive = -1, int roleId = -1)
        {
            ViewBag.name = name;
            ViewBag.tellNo = tellNo;
            ViewBag.isActive = isActive;
            ViewBag.roleId = roleId;
            return View();
        }
        //public ActionResult ListUser()
        //{
        //    return PartialView(db.User.Get());
        //}
        public ActionResult ViewUser()
        {
            return View();
        }

        public IActionResult Create()
        {
            ViewBag.IsActive = new SelectList(new[] { new { Value = "true", Text = "فعال" }, new { Value = "false", Text = "غیرفعال" }, }, "Value", "Text");
            ViewBag.RoleId = db.Role.Get();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(UserVm user, IFormFile imgup)
        {
            ViewBag.IsActive = new SelectList(new[] { new { Value = "true", Text = "فعال" }, new { Value = "false", Text = "غیرفعال" }, }, "Value", "Text", user.IsActive);
            ViewBag.RoleId = (db.Role.Get());
            if (ModelState.IsValid)
            {
                if (db.User.Get().Any(i => i.TellNo == user.TellNo))
                {
                    ModelState.AddModelError("TellNo", "شماره تلفن تکراریست");
                }
                else
                {
                    if (imgup != null)
                    {
                        user.ImageUrl = Guid.NewGuid().ToString() + Path.GetExtension(imgup.FileName);
                        string savePath = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot/Images/Users/", user.ImageUrl
                        );
                        using (var stream = new FileStream(savePath, FileMode.Create))
                        {
                            await imgup.CopyToAsync(stream);
                        }
                    }
                    TblUser addUser = new TblUser();
                    addUser.Auth = Guid.NewGuid().ToString();
                    addUser.RoleId = user.RoleId;
                    addUser.Name = user.Name;
                    addUser.IsActive = user.IsActive;
                    addUser.Password = PasswordHelper.EncodePasswordMd5(user.Password);
                    addUser.TellNo = user.TellNo;
                    addUser.ImageUrl = user.ImageUrl;
                    db.User.Add(addUser);
                    db.User.Save();
                    return await Task.FromResult(RedirectToAction(nameof(Index)));
                }
            }
            return await Task.FromResult(View(user));
        }
        public IActionResult ListUser()
        {
            return ViewComponent("UserListInAdminVc");
        }
        public async Task<string> ActiveDisableUser(int id)
        {
            TblUser updateUser = db.User.GetById(id);
            updateUser.IsActive = !updateUser.IsActive;
            db.User.Update(updateUser);
            db.User.Save();
            return await Task.FromResult("true");
        }
        public async Task<IActionResult> ChangePassword(int id)
        {
            ViewBag.UserName = db.User.GetById(id).Name;
            return await Task.FromResult(ViewComponent("ChangePasswordUser", new { id = id }));
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(VmChangePassword pass)
        {
            if (ModelState.IsValid)
            {
                TblUser updateUser = db.User.GetById(pass.Id);
                updateUser.Password = PasswordHelper.EncodePasswordMd5(pass.Password);
                db.User.Update(updateUser);
                return await Task.FromResult(RedirectToAction(nameof(Index)));
            }
            return PartialView("ChangePassword", pass);
        }


        public async Task<string> Delete(int id)
        {
            TblUser selectUserById = db.User.GetById(id);
            bool delete = db.User.Delete(selectUserById);
            if (delete)
            {
                db.User.Save();
                return await Task.FromResult("true");
            }
            return await Task.FromResult("خطا در حذف   لطفا بررسی فرمایید");
        }
        public async Task<ActionResult> Edit(int id)
        {
            TblUser selectUserById = db.User.GetById(id);
            EditUserVm User = new EditUserVm()
            {
                UserId = selectUserById.UserId,
                Auth = selectUserById.Auth,
                IsActive = selectUserById.IsActive,
                Name = selectUserById.Name,
                RoleId = selectUserById.RoleId,
                TellNo = selectUserById.TellNo,
                ImageUrl = selectUserById.ImageUrl,
            };
            ViewBag.IsActive = new SelectList(new[] { new { Value = "true", Text = "فعال" }, new { Value = "false", Text = "غیرفعال" }, }, "Value", "Text", selectUserById.IsActive);
            ViewBag.RoleId = (db.Role.Get());
            return await Task.FromResult(View(User));
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(EditUserVm user, IFormFile imgup)
        {
            ViewBag.IsActive = new SelectList(new[] { new { Value = "true", Text = "فعال" }, new { Value = "false", Text = "غیرفعال" }, }, "Value", "Text", user.IsActive);
            ViewBag.RoleId = (db.Role.Get());
            if (ModelState.IsValid)
            {
                if (db.User.Get().Any(i => i.UserId != user.UserId && i.TellNo == user.TellNo))
                {
                    ModelState.AddModelError("TellNo", "شماره تلفن تکراریست");
                }
                else
                {
                    if (imgup != null)
                    {
                        if (user.ImageUrl == null)
                        {
                            user.ImageUrl = Guid.NewGuid().ToString() + Path.GetExtension(imgup.FileName);
                        }
                        string savePath = Path.Combine(
                           Directory.GetCurrentDirectory(), "wwwroot/Images/Users/", user.ImageUrl
                       );
                        using (var stream = new FileStream(savePath, FileMode.Create))
                        {
                            await imgup.CopyToAsync(stream);
                        }
                    }
                    TblUser addUser = db.User.GetById(user.UserId);
                    addUser.RoleId = user.RoleId;
                    addUser.Name = user.Name;
                    addUser.IsActive = user.IsActive;
                    addUser.TellNo = user.TellNo;
                    addUser.ImageUrl = user.ImageUrl;
                    db.User.Update(addUser);
                    db.User.Save();
                    return await Task.FromResult(RedirectToAction(nameof(Index)));
                }
            }
            return await Task.FromResult(View(user));
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(MdUser user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        user.Email = user.Email.Trim().ToLower().Replace(" ", "");
        //        user.TellNo = user.TellNo.Trim().ToLower().Replace(" ", "");
        //        if (db.User.Get().Where(i => i.UserId != user.UserId).Any(i => i.TellNo == user.TellNo))
        //        {
        //            ModelState.AddModelError("TelNo", "شماره موبایل تکراریست");
        //        }
        //        else if (db.User.Get().Any(i => i.Email == user.Email && i.UserId != user.UserId))
        //        {
        //            ModelState.AddModelError("Email", "ایمیل تکراریست");
        //        }
        //        else
        //        {

        //            TblUser updateUser = db.User.GetById(user.UserId);
        //            updateUser.Balance = user.Balance;
        //            updateUser.Auth = Guid.NewGuid().ToString();
        //            updateUser.IsActive = user.IsActive;
        //            updateUser.Email = user.Email;
        //            updateUser.Name = user.Name;
        //            updateUser.RoleId = user.RoleId;
        //            updateUser.TellNo = user.TellNo;
        //            updateUser.DocsId = user.DocsId;
        //            updateUser.Password = user.Password;
        //            db.User.Update(updateUser);
        //            db.User.Save();
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    ViewBag.IsActive = new SelectList(new[] { new { Value = "1", Text = "فعال" }, new { Value = "0", Text = "غیرفعال" }, }, "Value", "Text", user.IsActive);
        //    ViewBag.RoleName = new SelectList(new[]
        //    { new { Value = "0", Text = "کارآموز" },
        //     new { Value = "1", Text = "مربی" },
        //    new { Value = "2", Text = "آموزشگاه" },
        //    new { Value = "3", Text = "مدیر" }}, "Value", "Text", user.RoleId);
        //    return View(user);
        //}
        //public ActionResult ListUser(string name = "", string tellNo = "", string email = "", string balance = "", int isActive = -1, int roleId = -1)
        //{
        //    List<TblUser> list = new List<TblUser>();
        //    list.AddRange(db.User.Get());
        //    if (name != "")
        //    {
        //        list = list.Where(p => p.Name.Contains(name)).ToList();
        //    }
        //    if (tellNo != "")
        //    {
        //        list = list.Where(p => p.TellNo.Contains(tellNo)).ToList();
        //    }
        //    if (email != "")
        //    {
        //        list = list.Where(p => p.Email.Contains(email)).ToList();
        //    }
        //    if (balance != "")
        //    {
        //        list = list.Where(p => p.Balance.ToString().Contains(balance)).ToList();
        //    }
        //    if (isActive > -1)
        //    {
        //        if (isActive == 1)
        //        {
        //            list = list.Where(p => p.IsActive == true).ToList();
        //        }
        //        else
        //        {
        //            list = list.Where(p => p.IsActive == false).ToList();
        //        }
        //    }
        //    if (roleId > -1)
        //    {
        //        if (roleId == 0)
        //        {
        //            list = list.Where(p => p.RoleId == 0).ToList();
        //        }
        //        else if (roleId == 1)
        //        {
        //            list = list.Where(p => p.RoleId == 1).ToList();
        //        }
        //        else if (roleId == 2)
        //        {
        //            list = list.Where(p => p.RoleId == 2).ToList();
        //        }
        //        else if (roleId == 3)
        //        {
        //            list = list.Where(p => p.RoleId == 3).ToList();
        //        }
        //    }
        //    return PartialView(list.OrderByDescending(i => i.UserId));
        //}



        public async Task<IActionResult> Search(string name=null, string tellNo = null, int isActive = -1, int roleId = -1)
        {
            List<TblUser> list = db.User.Get().ToList();
            if (name != null)
            {
                list = list.Where(i => i.Name.Contains(name)).ToList();
            }
            if (tellNo != null)
            {
                list = list.Where(i => i.TellNo.Contains(tellNo)).ToList();
            }
            if (isActive > -1)
            {
                if (isActive == 1)
                {
                    list = list.Where(p => p.IsActive == true).ToList();
                }
                else
                {
                    list = list.Where(p => p.IsActive == false).ToList();
                }
            }
            if (roleId > -1)
            {
                if (roleId == 0)
                {
                    list = list.Where(p => p.RoleId == 0).ToList();
                }
                else if (roleId == 1)
                {
                    list = list.Where(p => p.RoleId == 1).ToList();
                }
                else if (roleId == 2)
                {
                    list = list.Where(p => p.RoleId == 2).ToList();
                }
            }
            return await Task.FromResult(PartialView(list));
        }

    }
}
