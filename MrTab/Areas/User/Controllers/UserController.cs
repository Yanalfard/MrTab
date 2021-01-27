using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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


        public async Task<IActionResult> ChangePassword()
        {
            int id = Convert.ToInt32(User.Claims.First().Value);
            return await Task.FromResult(ViewComponent("ChangePasswordInUserVm", new { id = id }));
        }

        public IActionResult Menu()
        {
            return View();
        }
        public async Task<IActionResult> ToRestaurant()
        {
            return await Task.FromResult(View());
        }
        [HttpPost]
        public async Task<IActionResult> ToRestaurant(CreateRestaurantVm restaurant, IFormFile fileImage, IFormFile fileBanner)
        {
            if (ModelState.IsValid)
            {
                if (restaurant.CityId == 0)
                {
                    ModelState.AddModelError("CityId", "شهر را ثبت کنید");
                }
                else if (restaurant.CatagoryId == 0)
                {
                    ModelState.AddModelError("CatagoryId", "دسته بندی وجود ندارد  ");
                }
                else if (fileBanner == null)
                {
                    ModelState.AddModelError("MainBanner", "عکس بنر خالیست");
                }
                else if (fileImage == null)
                {
                    ModelState.AddModelError("MainImage", "عکس  خالیست");
                }
                else
                {
                    if (fileImage != null)
                    {
                        restaurant.MainImage = Guid.NewGuid().ToString() + Path.GetExtension(fileImage.FileName);
                        string savePath = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot/Images/Restaurant/", restaurant.MainImage
                        );
                        using (var stream = new FileStream(savePath, FileMode.Create))
                        {
                            await fileImage.CopyToAsync(stream);
                        }
                    }
                    if (fileBanner != null)
                    {
                        restaurant.MainBanner = Guid.NewGuid().ToString() + Path.GetExtension(fileBanner.FileName);
                        string savePath = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot/Images/Restaurant/", restaurant.MainBanner
                        );
                        using (var stream = new FileStream(savePath, FileMode.Create))
                        {
                            await fileBanner.CopyToAsync(stream);
                        }
                    }
                    TblRestaurant addRestaurant = new TblRestaurant();
                    addRestaurant.Address = restaurant.Address;
                    addRestaurant.CatagoryId = restaurant.CatagoryId;
                    addRestaurant.CityId = restaurant.CityId;
                    addRestaurant.InstagramUrl = restaurant.InstagramUrl;
                    addRestaurant.Lat = restaurant.Lat;
                    addRestaurant.Lon = restaurant.Lon;
                    addRestaurant.LongDesc = restaurant.LongDesc;
                    addRestaurant.MainBanner = restaurant.MainBanner;
                    addRestaurant.MainImage = restaurant.MainImage;
                    addRestaurant.Name = restaurant.Name;
                    addRestaurant.Neighborhood = restaurant.Neighborhood;
                    addRestaurant.ShortDesc = restaurant.ShortDesc;
                    addRestaurant.TellNo1 = restaurant.TellNo1;
                    addRestaurant.TellNo2 = restaurant.TellNo2;
                    addRestaurant.UserId = SelectUser().UserId;
                    addRestaurant.IsValid = false;
                    db.Restaurant.Add(addRestaurant);
                    db.Restaurant.Save();
                    if (restaurant.NameFood != null)
                    {
                        foreach (var item in restaurant.NameFood.Split('،'))
                        {
                            db.Food.Add(new TblFood()
                            {
                                RestaurantId = addRestaurant.RestaurantId,
                                Name = item,
                            });
                        }
                    }
                    if (restaurant.NameFoodType != null)
                    {
                        foreach (var item in restaurant.NameFoodType.Split('،'))
                        {
                            db.FoodType.Add(new TblFoodType()
                            {
                                RestaurantId = addRestaurant.RestaurantId,
                                Name = item,
                            });
                        }
                    }
                    if (restaurant.NameMealType != null)
                    {
                        foreach (var item in restaurant.NameMealType.Split('،'))
                        {
                            db.MealType.Add(new TblMealType()
                            {
                                RestaurantId = addRestaurant.RestaurantId,
                                Name = item,
                            });
                        }
                    }
                    if (restaurant.NameProperty != null)
                    {
                        foreach (var item in restaurant.NameProperty.Split('،'))
                        {
                            db.Property.Add(new TblProperty()
                            {
                                RestaurantId = addRestaurant.RestaurantId,
                                Name = item,
                            });
                        }
                    }
                    if (restaurant.NameWorkDay != null && restaurant.NameWorkTime != null)
                    {
                        db.WorkTime.Add(new TblWorkTime()
                        {
                            RestaurantId = addRestaurant.RestaurantId,
                            Day = restaurant.NameWorkDay,
                            Time = restaurant.NameWorkTime,
                        });
                    }
                    db.Food.Save();
                    return await Task.FromResult(RedirectToAction(nameof(Index)));

                }
            }
            return await Task.FromResult(View());
        }


        public async Task<IActionResult> Edit(int id)
        {
            TblRestaurant selectedRestaurant = db.Restaurant.Get().SingleOrDefault(i => i.RestaurantId == id && i.UserId == SelectUser().UserId);
            if (selectedRestaurant != null)
            {
                CreateRestaurantVm restaurant = new CreateRestaurantVm();
                restaurant.Address = selectedRestaurant.Address;
                restaurant.CatagoryId = selectedRestaurant.CatagoryId;
                restaurant.CityId = selectedRestaurant.CityId;
                restaurant.InstagramUrl = selectedRestaurant.InstagramUrl;
                restaurant.Lat = selectedRestaurant.Lat;
                restaurant.Lon = selectedRestaurant.Lon;
                restaurant.LongDesc = selectedRestaurant.LongDesc;
                restaurant.MainBanner = selectedRestaurant.MainBanner;
                restaurant.MainImage = selectedRestaurant.MainImage;
                restaurant.Name = selectedRestaurant.Name;
                restaurant.Neighborhood = selectedRestaurant.Neighborhood;
                restaurant.ShortDesc = selectedRestaurant.ShortDesc;
                restaurant.TellNo1 = selectedRestaurant.TellNo1;
                restaurant.TellNo2 = selectedRestaurant.TellNo2;
                restaurant.IsValid = selectedRestaurant.IsValid;
                restaurant.NameFood = string.Join(".", selectedRestaurant.TblFood.Select(t=>t.Name).ToList());
                restaurant.NameFoodType = string.Join(".", selectedRestaurant.TblFoodType.Select(t=>t.Name).ToList());
                restaurant.NameMealType = string.Join(".", selectedRestaurant.TblMealType.Select(t=>t.Name).ToList());
                restaurant.NameWorkDay = string.Join(".", selectedRestaurant.TblWorkTime.Select(t=>t.Time).ToList());
                restaurant.NameWorkTime = string.Join(".", selectedRestaurant.TblWorkTime.Select(t=>t.Day).ToList());
                return await Task.FromResult(View(selectedRestaurant));

            }
            return await Task.FromResult(RedirectToAction(nameof(Index)));

        }
        [HttpPost]
        public async Task<IActionResult> Edit(CreateRestaurantVm restaurant, IFormFile fileImage, IFormFile fileBanner)
        {
            if (ModelState.IsValid)
            {
                if (restaurant.CityId == 0)
                {
                    ModelState.AddModelError("CityId", "شهر را ثبت کنید");
                }
                else if (restaurant.CatagoryId == 0)
                {
                    ModelState.AddModelError("CatagoryId", "دسته بندی وجود ندارد  ");
                }
                else if (fileBanner == null)
                {
                    ModelState.AddModelError("MainBanner", "عکس بنر خالیست");
                }
                else if (fileImage == null)
                {
                    ModelState.AddModelError("MainImage", "عکس  خالیست");
                }
                else
                {
                    if (fileImage != null)
                    {
                        restaurant.MainImage = Guid.NewGuid().ToString() + Path.GetExtension(fileImage.FileName);
                        string savePath = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot/Images/Restaurant/", restaurant.MainImage
                        );
                        using (var stream = new FileStream(savePath, FileMode.Create))
                        {
                            await fileImage.CopyToAsync(stream);
                        }
                    }
                    if (fileBanner != null)
                    {
                        restaurant.MainBanner = Guid.NewGuid().ToString() + Path.GetExtension(fileBanner.FileName);
                        string savePath = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot/Images/Restaurant/", restaurant.MainBanner
                        );
                        using (var stream = new FileStream(savePath, FileMode.Create))
                        {
                            await fileBanner.CopyToAsync(stream);
                        }
                    }
                    TblRestaurant addRestaurant = new TblRestaurant();
                    addRestaurant.Address = restaurant.Address;
                    addRestaurant.CatagoryId = restaurant.CatagoryId;
                    addRestaurant.CityId = restaurant.CityId;
                    addRestaurant.InstagramUrl = restaurant.InstagramUrl;
                    addRestaurant.Lat = restaurant.Lat;
                    addRestaurant.Lon = restaurant.Lon;
                    addRestaurant.LongDesc = restaurant.LongDesc;
                    addRestaurant.MainBanner = restaurant.MainBanner;
                    addRestaurant.MainImage = restaurant.MainImage;
                    addRestaurant.Name = restaurant.Name;
                    addRestaurant.Neighborhood = restaurant.Neighborhood;
                    addRestaurant.ShortDesc = restaurant.ShortDesc;
                    addRestaurant.TellNo1 = restaurant.TellNo1;
                    addRestaurant.TellNo2 = restaurant.TellNo2;
                    addRestaurant.UserId = SelectUser().UserId;
                    addRestaurant.IsValid = false;
                    db.Restaurant.Add(addRestaurant);
                    db.Restaurant.Save();
                    if (restaurant.NameFood != null)
                    {
                        foreach (var item in restaurant.NameFood.Split('،'))
                        {
                            db.Food.Add(new TblFood()
                            {
                                RestaurantId = addRestaurant.RestaurantId,
                                Name = item,
                            });
                        }
                    }
                    if (restaurant.NameFoodType != null)
                    {
                        foreach (var item in restaurant.NameFoodType.Split('،'))
                        {
                            db.FoodType.Add(new TblFoodType()
                            {
                                RestaurantId = addRestaurant.RestaurantId,
                                Name = item,
                            });
                        }
                    }
                    if (restaurant.NameMealType != null)
                    {
                        foreach (var item in restaurant.NameMealType.Split('،'))
                        {
                            db.MealType.Add(new TblMealType()
                            {
                                RestaurantId = addRestaurant.RestaurantId,
                                Name = item,
                            });
                        }
                    }
                    if (restaurant.NameProperty != null)
                    {
                        foreach (var item in restaurant.NameProperty.Split('،'))
                        {
                            db.Property.Add(new TblProperty()
                            {
                                RestaurantId = addRestaurant.RestaurantId,
                                Name = item,
                            });
                        }
                    }
                    if (restaurant.NameWorkDay != null && restaurant.NameWorkTime != null)
                    {
                        db.WorkTime.Add(new TblWorkTime()
                        {
                            RestaurantId = addRestaurant.RestaurantId,
                            Day = restaurant.NameWorkDay,
                            Time = restaurant.NameWorkTime,
                        });
                    }
                    db.Food.Save();
                    return await Task.FromResult(RedirectToAction(nameof(Index)));

                }
            }
            return await Task.FromResult(View());
        }

        public async Task<string> DeleteRestauran(int id)
        {
            TblRestaurant selectedRestaurantById = db.Restaurant.Get().SingleOrDefault(i => i.UserId == SelectUser().UserId && i.RestaurantId == id);
            if (selectedRestaurantById != null)
            {
                bool delete = db.Restaurant.Delete(selectedRestaurantById);
                if (delete)
                {
                    if (selectedRestaurantById.MainImage != null)
                    {
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Restaurant/", selectedRestaurantById.MainImage);
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    if (selectedRestaurantById.MainBanner != null)
                    {
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Restaurant/", selectedRestaurantById.MainBanner);
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    List<TblImage> selectImages = db.Image.Get().Where(i => i.RestaurantId == id).ToList();
                    if (selectImages.Count != 0)
                    {
                        foreach (var item in selectImages)
                        {
                            if (item.Url != null)
                            {
                                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Restaurant/Gallery/", item.Url);
                                if (System.IO.File.Exists(imagePath))
                                {
                                    System.IO.File.Delete(imagePath);
                                }
                            }
                        }
                    }
                    db.Restaurant.Save();
                    return await Task.FromResult("true");
                }

            }
            return await Task.FromResult("خطا در حذف   لطفا بررسی فرمایید");
        }

    }
}
