﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.ViewModel;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MrTab.Utilities;
using Newtonsoft.Json;
using Services.Security;
using Services.Services;

namespace MrTab.Controllers
{
    public class AccountController : Controller
    {
        private Core db = new Core();

        private readonly ICaptchaValidator _captchaValidator;

        public AccountController(ICaptchaValidator captchaValidator)
        {
            _captchaValidator = captchaValidator;
        }

        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.Claims.Last().Value == "user")
                {
                    return await Task.FromResult(Redirect("/User/User/Index"));
                }
                else if (User.Claims.Last().Value == "author")
                {
                    return await Task.FromResult(Redirect("/Admin/Home/Index"));
                }
                else if (User.Claims.Last().Value == "admin")
                {
                    return await Task.FromResult(Redirect("/Admin/Home/Index"));

                }
                else
                {
                    return await Task.FromResult(Redirect("/"));
                }
            }
            return await Task.FromResult(View());
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginVm login, string ReturnUrl = "/")
        {

            //if (!await _captchaValidator.IsCaptchaPassedAsync(login.Captcha))
            //{
            //    ModelState.AddModelError("TellNo", "ورود غیر مجاز");
            //    return View(login);
            //}

            if (ModelState.IsValid)
            {
                string password = PasswordHelper.EncodePasswordMd5(login.Password);
                if (db.User.Get().Any(i => i.TellNo == login.TellNo && i.Password == password))
                {
                    TblUser user = db.User.Get().FirstOrDefault(i => i.TellNo == login.TellNo);
                    if (user.IsActive)
                    {
                        var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                        new Claim(ClaimTypes.Name,user.TellNo),
                        new Claim(ClaimTypes.Role,db.Role.GetById(user.RoleId).Name.Trim()),
                    };
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);

                        var properties = new AuthenticationProperties
                        {
                            IsPersistent = login.RememberMe
                        };
                        await HttpContext.SignInAsync(principal, properties);
                        ViewBag.IsSuccess = true;
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("TellNo", "حساب کاربری شما فعال نیست");
                    }
                }
                else
                {
                    ModelState.AddModelError("TellNo", "شماره تلفن  یا رمز اشتباه است");
                }
            }
            return await Task.FromResult(View(login));
        }


        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
        public async Task<IActionResult> Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.Claims.Last().Value == "user")
                {
                    return await Task.FromResult(Redirect("/User/User/Index"));
                }
                else if (User.Claims.Last().Value == "author")
                {
                    return await Task.FromResult(Redirect("/Admin/Home/Index"));
                }
                else if (User.Claims.Last().Value == "admin")
                {
                    return await Task.FromResult(Redirect("/Admin/Home/Index"));

                }
                else
                {
                    return await Task.FromResult(Redirect("/"));
                }
            }
            return await Task.FromResult(View());
        }
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterVm register)
        {

            //if (!await _captchaValidator.IsCaptchaPassedAsync(register.Captcha))
            //{
            //    ModelState.AddModelError("Name", "ورود غیر مجاز");
            //    return View(register);
            //}
            if (ModelState.IsValid)
            {
                if (!db.User.Get().Any(i => i.TellNo == register.TellNo))
                {
                    var CodeCreator = Guid.NewGuid().ToString();
                    //   string Code = CodeCreator.Substring(CodeCreator.Length - 5);
                    string Code = new Random().Next(10000, 99999).ToString();
                    TblUser addUser = new TblUser();
                    addUser.Auth = Code;
                    addUser.RoleId = 0;
                    addUser.Name = "";
                    addUser.IsActive = false;
                    addUser.Password = PasswordHelper.EncodePasswordMd5(register.Password);
                    addUser.Name = register.Name;
                    addUser.TellNo = register.TellNo;
                    db.User.Add(addUser);
                    db.User.Save();
                    string message = addUser.Auth;
                    await Sms.SendSms(addUser.TellNo, message, "FoodestanRegister");
                    return await Task.FromResult(Redirect("/Account/Validation?tellNo=" + addUser.TellNo));
                }
                else
                {
                    ModelState.AddModelError("TellNo", "شماره تلفن تکراریست");
                }
            }
            return await Task.FromResult(View(register));
        }
        public async Task<IActionResult> Validation(string tellNo)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.Claims.Last().Value == "user")
                {
                    return await Task.FromResult(Redirect("/User/User/Index"));
                }
                else if (User.Claims.Last().Value == "author")
                {
                    return await Task.FromResult(Redirect("/Admin/Home/Index"));
                }
                else if (User.Claims.Last().Value == "admin")
                {
                    return await Task.FromResult(Redirect("/Admin/Home/Index"));

                }
                else
                {
                    return await Task.FromResult(Redirect("/"));
                }
            }
            return await Task.FromResult(View(new ActiveVm()
            {
                Tell = tellNo
            }));
        }
        [HttpPost]
        public async Task<IActionResult> Validation(ActiveVm active)
        {
            if (ModelState.IsValid)
            {
                if (db.User.Get().Any(i => i.TellNo == active.Tell))
                {
                    TblUser selectedUser = db.User.Get().FirstOrDefault(i => i.TellNo == active.Tell);
                    if (selectedUser.Auth == active.Auth)
                    {
                        selectedUser.IsActive = true;
                        var CodeCreator = Guid.NewGuid().ToString();
                        //   string Code = CodeCreator.Substring(CodeCreator.Length - 5);
                        string Code = new Random().Next(10000, 99999).ToString();
                        selectedUser.Auth = Code;
                        db.User.Save();
                        return await Task.FromResult(Redirect("/Account/Login?Active=true"));
                    }
                    else
                    {
                        ModelState.AddModelError("Auth", "کد فعال سازی اشتباه است");
                    }
                }
                else
                {
                    ModelState.AddModelError("id", "شماره تلفن یافت نشد");
                }
            }
            return await Task.FromResult(View(active));
        }

        public async Task<IActionResult> ForgotPassword()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.Claims.Last().Value == "user")
                {
                    return await Task.FromResult(Redirect("/User/User/Index"));
                }
                else if (User.Claims.Last().Value == "author")
                {
                    return await Task.FromResult(Redirect("/Admin/Home/Index"));
                }
                else if (User.Claims.Last().Value == "admin")
                {
                    return await Task.FromResult(Redirect("/Admin/Home/Index"));

                }
                else
                {
                    return await Task.FromResult(Redirect("/"));
                }
            }
            return await Task.FromResult(View());
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVm forget)
        {
            //if (!await _captchaValidator.IsCaptchaPassedAsync(forget.Captcha))
            //{
            //    ModelState.AddModelError("TellNo", "ورود غیر مجاز");
            //    return View(forget);
            //}
            if (ModelState.IsValid)
            {
                if (db.User.Get().Any(i => i.TellNo == forget.TellNo))
                {
                    TblUser forgotPassword = db.User.Get().FirstOrDefault(i => i.TellNo == forget.TellNo);
                    string message = forgotPassword.Auth;
                    await Sms.SendSms(forgotPassword.TellNo, message, "FoodestanForget");
                    return await Task.FromResult(Redirect("/Account/RestorePassword?tell=" + forgotPassword.TellNo));
                }
                else
                {
                    ModelState.AddModelError("TellNo", "شماره تلفن یافت نشد");
                }
            }
            return await Task.FromResult(View(forget));
        }

        public async Task<IActionResult> RestorePassword(string tell)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.Claims.Last().Value == "user")
                {
                    return await Task.FromResult(Redirect("/User/User/Index"));
                }
                else if (User.Claims.Last().Value == "author")
                {
                    return await Task.FromResult(Redirect("/Admin/Home/Index"));
                }
                else if (User.Claims.Last().Value == "admin")
                {
                    return await Task.FromResult(Redirect("/Admin/Home/Index"));

                }
                else
                {
                    return await Task.FromResult(Redirect("/"));
                }
            }
            return await Task.FromResult(View(new ActiveVm()
            {
                Tell = tell
            }));
        }
        [HttpPost]
        public async Task<IActionResult> RestorePassword(ActiveVm active)
        {
            if (ModelState.IsValid)
            {
                if (db.User.Get().Any(i => i.TellNo == active.Tell))
                {
                    TblUser selectedUser = db.User.Get().FirstOrDefault(i => i.TellNo == active.Tell);
                    if (selectedUser.Auth == active.Auth)
                    {
                        return await Task.FromResult(Redirect("/Account/ChangePassword?tellNo=" + selectedUser.TellNo + "&&auth=" + selectedUser.Auth));
                    }
                    else
                    {
                        ModelState.AddModelError("Auth", "کد تغیر رمز  اشتباه است");
                    }
                }
                else
                {
                    ModelState.AddModelError("Auth", "شماره تلفن یافت نشد");
                }
            }
            return await Task.FromResult(View(active));
        }
        public async Task<IActionResult> ChangePassword(string tellNo, string auth)
        {
            return await Task.FromResult(View(new ChangePasswordVm()
            {
                Tell = tellNo,
                Auth = auth
            }));
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVm change)
        {
            if (ModelState.IsValid)
            {
                if (db.User.Get().Any(i => i.TellNo == change.Tell && i.Auth == change.Auth))
                {
                    TblUser selectedUser = db.User.Get().FirstOrDefault(i => i.TellNo == change.Tell);
                    selectedUser.IsActive = true;
                    selectedUser.Password = PasswordHelper.EncodePasswordMd5(change.Password);
                    var CodeCreator = Guid.NewGuid().ToString();
                    //  string Code = CodeCreator.Substring(CodeCreator.Length - 5);
                    string Code = new Random().Next(10000, 99999).ToString();
                    selectedUser.Auth = Code;
                    db.User.Update(selectedUser);
                    db.User.Save();
                    return await Task.FromResult(Redirect("/Account/Login?ChangePassword=true"));
                }
                else
                {
                    ModelState.AddModelError("Password", "شماره تلفن یا کد فعال سازی اشتباه است");
                }
            }
            return await Task.FromResult(View(change));
        }
        public IActionResult CodeSent()
        {
            return View();
        }

    }
}
