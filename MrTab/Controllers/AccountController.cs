using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MrTab.Utilities;
using Services.Security;
using Services.Services;

namespace MrTab.Controllers
{
    public class AccountController : Controller
    {
        private Core db = new Core();
        public async Task<IActionResult> Login()
        {
            return await Task.FromResult(View());
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginVm login)
        {
            if (ModelState.IsValid)
            {
                string password = PasswordHelper.EncodePasswordMd5(login.Password);
                if (db.User.Get().Any(i => i.TellNo == login.TellNo && i.Password == password))
                {
                    TblUser user = db.User.Get().FirstOrDefault(i => i.TellNo == login.TellNo);
                    if (user.IsActive)
                    {
                        await SignInAsync(user);
                        return Redirect("/");
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

        private async Task SignInAsync(TblUser tblUser)
        {
            var UserClaim = GetClaims(tblUser);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                          new ClaimsPrincipal(UserClaim),
                                          new AuthenticationProperties
                                          {
                                              AllowRefresh = true,
                                              IsPersistent = true,
                                              ExpiresUtc = DateTime.Now.AddDays(30)
                                          });
        }

        private ClaimsIdentity GetClaims(TblUser tblUser)
        {
            return new ClaimsIdentity(new List<Claim>
                    {
                        new Claim("RoleId",db.Role.GetById(tblUser.RoleId).Name.Trim()),
                        new Claim("TellNo",tblUser.TellNo),
                    }, CookieAuthenticationDefaults.AuthenticationScheme);
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
        public async Task<IActionResult> Register()
        {
            return await Task.FromResult(View());
        }
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterVm register)
        {
            if (ModelState.IsValid)
            {
                if (!db.User.Get().Any(i => i.TellNo == register.TellNo))
                {
                    var CodeCreator = Guid.NewGuid().ToString();
                    string Code = CodeCreator.Substring(CodeCreator.Length - 5);
                    TblUser addUser = new TblUser();
                    addUser.Auth = Code;
                    addUser.RoleId = 0;
                    addUser.Name = "";
                    addUser.IsActive = false;
                    addUser.Password = PasswordHelper.EncodePasswordMd5(register.Password);
                    addUser.TellNo = register.TellNo;
                    db.User.Add(addUser);
                    db.User.Save();
                    string message = addUser.Auth;
                    //await Sms.SendSms(addUser.TellNo, message, "RegisterMrTab");
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
                    if (selectedUser.Auth == active.Id)
                    {
                        selectedUser.IsActive = true;
                        var CodeCreator = Guid.NewGuid().ToString();
                        string Code = CodeCreator.Substring(CodeCreator.Length - 5);
                        selectedUser.Auth = Code;
                        db.User.Save();
                        return await Task.FromResult(Redirect("/Account/Login?Active=true"));
                    }
                    else
                    {
                        ModelState.AddModelError("id", "کد فعال سازی اشتباه است");
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
            return await Task.FromResult(View());
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVm forget)
        {
            if (ModelState.IsValid)
            {
                if (db.User.Get().Any(i => i.TellNo == forget.TellNo))
                {
                    TblUser forgotPassword = db.User.Get().FirstOrDefault(i => i.TellNo == forget.TellNo);
                    string message = forgotPassword.Auth;
                    //await Sms.SendSms(addUser.TellNo, message, "RegisterMrTab");
                    return await Task.FromResult(Redirect("/Account/RestorePassword?tell=" + forgotPassword.TellNo));
                }
                else
                {
                    ModelState.AddModelError("id", "شماره تلفن یافت نشد");
                }
            }
            return await Task.FromResult(View(forget));
        }

        public async Task<IActionResult> RestorePassword(string tell)
        {
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
                    if (selectedUser.Auth == active.Id)
                    {
                        return await Task.FromResult(Redirect("/Account/ChangePassword?tellNo=" + selectedUser.TellNo + "&&auth=" + selectedUser.Auth));
                    }
                    else
                    {
                        ModelState.AddModelError("id", "کد فعال سازی اشتباه است");
                    }
                }
                else
                {
                    ModelState.AddModelError("id", "شماره تلفن یافت نشد");
                }
            }
            return await Task.FromResult(View(active));
        }
        public async Task<IActionResult> ChangePassword(string tellNo, string auth)
        {
            return await Task.FromResult(View(new ChangePasswordVm()
            {
                Tell = tellNo,
                Id = auth
            }));
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVm change)
        {
            if (ModelState.IsValid)
            {
                if (db.User.Get().Any(i => i.TellNo == change.Tell && i.Auth==change.Id))
                {
                    TblUser selectedUser = db.User.Get().FirstOrDefault(i => i.TellNo == change.Tell);
                    selectedUser.Password = PasswordHelper.EncodePasswordMd5(change.Password);
                    db.User.Update(selectedUser);
                    db.User.Save();
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
