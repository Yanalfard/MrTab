﻿using System;
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
                        /// #region resize Image
                        ImageConvertor imgResizer = new ImageConvertor();
                        string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Restaurant/thumb", restaurant.MainImage);
                         imgResizer.Image_resize(savePath, thumbPath, 200);
                        /// #endregion
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
                        /// #region resize Image
                        ImageConvertor imgResizer = new ImageConvertor();
                        string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Restaurant/thumb", restaurant.MainBanner);
                         imgResizer.Image_resize(savePath, thumbPath, 200);
                        /// #endregion
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
                restaurant.RestaurantId = selectedRestaurant.RestaurantId;
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
                restaurant.NameCity = selectedRestaurant.City.Name;
                restaurant.NameCatagory = selectedRestaurant.Catagory.Name;
                restaurant.Neighborhood = selectedRestaurant.Neighborhood;
                restaurant.ShortDesc = selectedRestaurant.ShortDesc;
                restaurant.TellNo1 = selectedRestaurant.TellNo1;
                restaurant.TellNo2 = selectedRestaurant.TellNo2;
                restaurant.IsValid = selectedRestaurant.IsValid;
                restaurant.NameFood = string.Join("،", selectedRestaurant.TblFood.Select(t => t.Name).ToList());
                restaurant.NameFoodType = string.Join("،", selectedRestaurant.TblFoodType.Select(t => t.Name).ToList());
                restaurant.NameMealType = string.Join("،", selectedRestaurant.TblMealType.Select(t => t.Name).ToList());
                restaurant.NameProperty = string.Join("،", selectedRestaurant.TblProperty.Select(t => t.Name).ToList());
                restaurant.NameWorkDay = string.Join("،", selectedRestaurant.TblWorkTime.Select(t => t.Day).ToList());
                restaurant.NameWorkTime = string.Join("،", selectedRestaurant.TblWorkTime.Select(t => t.Time).ToList());
                return await Task.FromResult(View(restaurant));

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
                else
                {
                    if (fileImage != null)
                    {
                        if (restaurant.MainImage != null)
                        {
                            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Restaurant/", restaurant.MainImage);
                            if (System.IO.File.Exists(imagePath))
                            {
                                System.IO.File.Delete(imagePath);
                            }
                            var imagePath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Restaurant/thumb", restaurant.MainImage);
                            if (System.IO.File.Exists(imagePath2))
                            {
                                System.IO.File.Delete(imagePath2);
                            }
                        }
                        restaurant.MainImage = Guid.NewGuid().ToString() + Path.GetExtension(fileImage.FileName);
                        string savePath = Path.Combine(
                           Directory.GetCurrentDirectory(), "wwwroot/Images/Restaurant/", restaurant.MainImage
                       );
                        using (var stream = new FileStream(savePath, FileMode.Create))
                        {
                            await fileImage.CopyToAsync(stream);
                        }
                        /// #region resize Image
                        ImageConvertor imgResizer = new ImageConvertor();
                        string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Restaurant/thumb", restaurant.MainImage);
                         imgResizer.Image_resize(savePath, thumbPath, 200);
                        /// #endregion
                    }
                    if (fileBanner != null)
                    {
                        if (restaurant.MainBanner != null)
                        {
                            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Restaurant/", restaurant.MainBanner);
                            if (System.IO.File.Exists(imagePath))
                            {
                                System.IO.File.Delete(imagePath);
                            }
                            var imagePath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Restaurant/thumb", restaurant.MainBanner);
                            if (System.IO.File.Exists(imagePath2))
                            {
                                System.IO.File.Delete(imagePath2);
                            }
                        }
                        restaurant.MainBanner = Guid.NewGuid().ToString() + Path.GetExtension(fileBanner.FileName);
                        string savePath = Path.Combine(
                           Directory.GetCurrentDirectory(), "wwwroot/Images/Restaurant/", restaurant.MainBanner
                       );
                        using (var stream = new FileStream(savePath, FileMode.Create))
                        {
                            await fileBanner.CopyToAsync(stream);
                        }
                        /// #region resize Image
                        ImageConvertor imgResizer = new ImageConvertor();
                        string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Restaurant/thumb", restaurant.MainBanner);
                         imgResizer.Image_resize(savePath, thumbPath, 200);
                        /// #endregion
                    }
                    TblRestaurant updateRestaurant = db.Restaurant.GetById(restaurant.RestaurantId);
                    updateRestaurant.Address = restaurant.Address;
                    updateRestaurant.CatagoryId = restaurant.CatagoryId;
                    updateRestaurant.CityId = restaurant.CityId;
                    updateRestaurant.InstagramUrl = restaurant.InstagramUrl;
                    updateRestaurant.Lat = restaurant.Lat;
                    updateRestaurant.Lon = restaurant.Lon;
                    updateRestaurant.LongDesc = restaurant.LongDesc;
                    updateRestaurant.MainBanner = restaurant.MainBanner;
                    updateRestaurant.MainImage = restaurant.MainImage;
                    updateRestaurant.Name = restaurant.Name;
                    updateRestaurant.Neighborhood = restaurant.Neighborhood;
                    updateRestaurant.ShortDesc = restaurant.ShortDesc;
                    updateRestaurant.TellNo1 = restaurant.TellNo1;
                    updateRestaurant.TellNo2 = restaurant.TellNo2;
                    updateRestaurant.UserId = SelectUser().UserId;
                    updateRestaurant.IsValid = false;
                    db.Restaurant.Update(updateRestaurant);
                    db.Restaurant.Save();
                    db.Food.Get().Where(t => t.RestaurantId == updateRestaurant.RestaurantId).ToList().ForEach(t => db.Food.Delete(t));
                    if (restaurant.NameFood != null)
                    {
                        foreach (var item in restaurant.NameFood.Split('،'))
                        {
                            db.Food.Add(new TblFood()
                            {
                                RestaurantId = updateRestaurant.RestaurantId,
                                Name = item,
                            });
                        }
                    }
                    db.FoodType.Get().Where(t => t.RestaurantId == updateRestaurant.RestaurantId).ToList().ForEach(t => db.FoodType.Delete(t));
                    if (restaurant.NameFoodType != null)
                    {
                        foreach (var item in restaurant.NameFoodType.Split('،'))
                        {
                            db.FoodType.Add(new TblFoodType()
                            {
                                RestaurantId = updateRestaurant.RestaurantId,
                                Name = item,
                            });
                        }
                    }
                    db.MealType.Get().Where(t => t.RestaurantId == updateRestaurant.RestaurantId).ToList().ForEach(t => db.MealType.Delete(t));
                    if (restaurant.NameMealType != null)
                    {
                        foreach (var item in restaurant.NameMealType.Split('،'))
                        {
                            db.MealType.Add(new TblMealType()
                            {
                                RestaurantId = updateRestaurant.RestaurantId,
                                Name = item,
                            });
                        }
                    }
                    db.Property.Get().Where(t => t.RestaurantId == updateRestaurant.RestaurantId).ToList().ForEach(t => db.Property.Delete(t));
                    if (restaurant.NameProperty != null)
                    {
                        foreach (var item in restaurant.NameProperty.Split('،'))
                        {
                            db.Property.Add(new TblProperty()
                            {
                                RestaurantId = updateRestaurant.RestaurantId,
                                Name = item,
                            });
                        }
                    }
                    if (restaurant.NameWorkDay != null && restaurant.NameWorkTime != null)
                    {
                        db.WorkTime.Add(new TblWorkTime()
                        {
                            RestaurantId = updateRestaurant.RestaurantId,
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
                        var imagePath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Restaurant/thumb", selectedRestaurantById.MainImage);
                        if (System.IO.File.Exists(imagePath2))
                        {
                            System.IO.File.Delete(imagePath2);
                        }
                    }
                    if (selectedRestaurantById.MainBanner != null)
                    {
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Restaurant/", selectedRestaurantById.MainBanner);
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                        var imagePath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Restaurant/thumb", selectedRestaurantById.MainBanner);
                        if (System.IO.File.Exists(imagePath2))
                        {
                            System.IO.File.Delete(imagePath2);
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
                                var imagePath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Restaurant/Gallery/thumb", item.Url);
                                if (System.IO.File.Exists(imagePath2))
                                {
                                    System.IO.File.Delete(imagePath2);
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




        public async Task<IActionResult> Gallary(int id)
        {
            ViewBag.RestaurantId = id;
            List<TblImage> selectedImages = db.Image.Get(i => i.RestaurantId == id && i.Restaurant.UserId == SelectUser().UserId).ToList();
            return await Task.FromResult(View(selectedImages));
        }

        public async Task<IActionResult> AddImage(TblImage Image, IFormFile fileImage)
        {
            if (fileImage != null)
            {
                bool check = db.Restaurant.Get().Any(i => i.RestaurantId == Image.RestaurantId && i.UserId == SelectUser().UserId);
                if (check)
                {
                    Image.Url = Guid.NewGuid().ToString() + Path.GetExtension(fileImage.FileName);
                    string savePath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/Images/Restaurant/Gallery/", Image.Url
                    );
                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await fileImage.CopyToAsync(stream);
                    }
                    /// #region resize Image
                    ImageConvertor imgResizer = new ImageConvertor();
                    string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Restaurant/Gallery/thumb", Image.Url);
                     imgResizer.Image_resize(savePath, thumbPath, 200);
                    /// #endregion
                    db.Image.Add(new TblImage()
                    {
                        Url = Image.Url,
                        Status = 1,
                        IsValid = false,
                        RestaurantId = Image.RestaurantId,
                    });
                    db.Image.Save();
                }
            }
            return await Task.FromResult(Redirect("/User/User/Gallary/" + Image.RestaurantId));
        }

        public async Task<string> DeleteGallary(int id)
        {
            TblImage selectedRestaurantById = db.Image.Get().SingleOrDefault(i => i.Restaurant.UserId == SelectUser().UserId && i.ImageId == id);
            if (selectedRestaurantById != null)
            {
                bool delete = db.Image.Delete(selectedRestaurantById);
                if (delete)
                {
                    if (selectedRestaurantById.Url != null)
                    {
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Restaurant/Gallery/", selectedRestaurantById.Url);
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                        var imagePath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Restaurant/Gallery/thumb", selectedRestaurantById.Url);
                        if (System.IO.File.Exists(imagePath2))
                        {
                            System.IO.File.Delete(imagePath2);
                        }

                    }
                    db.Image.Save();
                    return await Task.FromResult("true");
                }
            }
            return await Task.FromResult("خطا در حذف   لطفا بررسی فرمایید");
        }
    }
}
