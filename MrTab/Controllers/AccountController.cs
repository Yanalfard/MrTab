using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.ViewModel;
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

        private async Task<bool> IsCaptchaValid(string response)
        {
            try
            {
                //Localhost
                var secret = "6LfeB-IZAAAAAFJGzrD4-Vz9B4GPnjaps0gjQwFq";
                //Site
                //var secret = "6LfeB-IZAAAAAFJGzrD4-Vz9B4GPnjaps0gjQwFq";
                using (var client = new HttpClient())
                {

                    var values = new Dictionary<string, string>
                    {
                        {"secret", secret},
                        {"response", response},
                        //{"remoteip", Request.UserHostAddress}
                    };

                    var content = new FormUrlEncodedContent(values);
                    var verify = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", content);

                    //return verify.IsSuccessStatusCode;

                    var captchaResponseJson = await verify.Content.ReadAsStringAsync();
                    var captchaResult = JsonConvert.DeserializeObject<CaptchaResponseViewModel>(captchaResponseJson);
                    return captchaResult.Success
                           && captchaResult.Action == "contact_us"
                           && captchaResult.Score > 0.5;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
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
                        var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                        new Claim(ClaimTypes.Name,user.TellNo),
                        new Claim("RoleId",db.Role.GetById(user.RoleId).Name.Trim()),
                    };
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);

                        var properties = new AuthenticationProperties
                        {
                            IsPersistent = login.RememberMe
                        };
                        HttpContext.SignInAsync(principal, properties);

                        ViewBag.IsSuccess = true;
                        return View();
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

        //private async Task SignInAsync(TblUser tblUser)
        //{
        //    var UserClaim = GetClaims(tblUser);

        //    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
        //                                  new ClaimsPrincipal(UserClaim),
        //                                  new AuthenticationProperties
        //                                  {
        //                                      AllowRefresh = true,
        //                                      IsPersistent = true,
        //                                      ExpiresUtc = DateTime.Now.AddDays(30)
        //                                  });
        //}

        //private ClaimsIdentity GetClaims(TblUser tblUser)
        //{
        //    return new ClaimsIdentity(new List<Claim>
        //            {
        //                new Claim("RoleId",db.Role.GetById(tblUser.RoleId).Name.Trim()),
        //                new Claim("TellNo",tblUser.TellNo),
        //            }, CookieAuthenticationDefaults.AuthenticationScheme);
        //}
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
                    if (selectedUser.Auth == active.Auth)
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
                    ModelState.AddModelError("TellNo", "شماره تلفن یافت نشد");
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
                    selectedUser.Password = PasswordHelper.EncodePasswordMd5(change.Password);
                    var CodeCreator = Guid.NewGuid().ToString();
                    string Code = CodeCreator.Substring(CodeCreator.Length - 5);
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
