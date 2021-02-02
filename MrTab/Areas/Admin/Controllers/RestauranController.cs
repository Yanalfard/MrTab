using DataLayer.Metadata;
using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MrTab.Utilities;
using Services.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MrTab.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("author,admin")]
    public class RestauranController : Controller
    {
        private Core db = new Core();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult IsValid()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.CityId = db.City.Get();
            ViewBag.UserId = db.User.Get().Where(i => i.RoleId != 3);
            ViewBag.CatagoryId = db.Catagory.Get();
            return await Task.FromResult(View());
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(MdRestaurant restaurant, IFormFile MainImage, IFormFile MainBanner)
        {
            ViewBag.CityId = db.City.Get();
            ViewBag.UserId = db.User.Get().Where(i => i.RoleId != 3);
            ViewBag.CatagoryId = db.Catagory.Get();
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
                else if (restaurant.UserId == 0)
                {
                    ModelState.AddModelError("UserId", " کاربری وجود ندارد ");
                }
                else if (MainBanner == null)
                {
                    ModelState.AddModelError("MainBanner", "عکس بنر خالیست");
                }
                else if (MainImage == null)
                {
                    ModelState.AddModelError("MainImage", "عکس  خالیست");
                }
                else if (MainImage.Length > 20485760)
                {
                    ModelState.AddModelError("MainImage", "حجم عکس بیشتر از 2 مگابایات است");
                    return await Task.FromResult(RedirectToAction(nameof(Index)));
                }
                else if (!MainImage.IsImage())
                {
                    ModelState.AddModelError("MainImage", "عکس معتبر نمی باشد");
                    return await Task.FromResult(RedirectToAction(nameof(Index)));
                }
                else if (MainBanner.Length > 20485760)
                {
                    ModelState.AddModelError("MainBanner", "حجم عکس بیشتر از 2 مگابایات است");
                    return await Task.FromResult(RedirectToAction(nameof(Index)));
                }
                else if (!MainBanner.IsImage())
                {
                    ModelState.AddModelError("MainBanner", "عکس معتبر نمی باشد");
                    return await Task.FromResult(RedirectToAction(nameof(Index)));
                }
                else
                {
                    if (MainImage != null)
                    {
                        restaurant.MainImage = Guid.NewGuid().ToString() + Path.GetExtension(MainImage.FileName);
                        string savePath = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot/Images/Restaurant/", restaurant.MainImage
                        );
                        using (var stream = new FileStream(savePath, FileMode.Create))
                        {
                            await MainImage.CopyToAsync(stream);
                        }
                    }
                    if (MainBanner != null)
                    {
                        restaurant.MainBanner = Guid.NewGuid().ToString() + Path.GetExtension(MainBanner.FileName);
                        string savePath = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot/Images/Restaurant/", restaurant.MainBanner
                        );
                        using (var stream = new FileStream(savePath, FileMode.Create))
                        {
                            await MainBanner.CopyToAsync(stream);
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
                    addRestaurant.UserId = restaurant.UserId;
                    addRestaurant.IsValid = true;
                    db.Restaurant.Add(addRestaurant);
                    db.Restaurant.Save();
                    return await Task.FromResult(RedirectToAction(nameof(Index)));
                }
            }
            return await Task.FromResult(View(restaurant));
        }

        public async Task<IActionResult> Edit(int id)
        {
            TblRestaurant selectedRestaurant = db.Restaurant.GetById(id);
            MdRestaurant md = new MdRestaurant();
            md.RestaurantId = selectedRestaurant.RestaurantId;
            md.Address = selectedRestaurant.Address;
            md.CatagoryId = selectedRestaurant.CatagoryId;
            md.CityId = selectedRestaurant.CityId;
            md.InstagramUrl = selectedRestaurant.InstagramUrl;
            md.Lat = selectedRestaurant.Lat;
            md.Lon = selectedRestaurant.Lon;
            md.LongDesc = selectedRestaurant.LongDesc;
            md.MainBanner = selectedRestaurant.MainBanner;
            md.MainImage = selectedRestaurant.MainImage;
            md.Name = selectedRestaurant.Name;
            md.Neighborhood = selectedRestaurant.Neighborhood;
            md.ShortDesc = selectedRestaurant.ShortDesc;
            md.TellNo1 = selectedRestaurant.TellNo1;
            md.TellNo2 = selectedRestaurant.TellNo2;
            md.UserId = selectedRestaurant.UserId;
            md.UserId = selectedRestaurant.UserId;

            ViewBag.CityId = db.City.Get();
            ViewBag.UserId = db.User.Get().Where(i => i.RoleId != 3);
            ViewBag.CatagoryId = db.Catagory.Get();
            return await Task.FromResult(View(md));
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(MdRestaurant restaurant, IFormFile MainImage, IFormFile MainBanner)
        {
            ViewBag.CityId = db.City.Get();
            ViewBag.UserId = db.User.Get().Where(i => i.RoleId != 3);
            ViewBag.CatagoryId = db.Catagory.Get();
            if (ModelState.IsValid)
            {
                if (MainImage != null)
                {
                    if (MainImage.Length > 20485760)
                    {
                        ModelState.AddModelError("MainImage", "حجم عکس بیشتر از 2 مگابایات است");
                        return await Task.FromResult(RedirectToAction(nameof(Index)));
                    }
                    else if (!MainImage.IsImage())
                    {
                        ModelState.AddModelError("MainImage", "عکس معتبر نمی باشد");
                        return await Task.FromResult(RedirectToAction(nameof(Index)));
                    }
                    if (restaurant.MainImage != null)
                    {
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Restaurant/", restaurant.MainImage);
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    restaurant.MainImage = Guid.NewGuid().ToString() + Path.GetExtension(MainImage.FileName);
                    string savePath = Path.Combine(
                       Directory.GetCurrentDirectory(), "wwwroot/Images/Restaurant/", restaurant.MainImage
                   );
                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await MainImage.CopyToAsync(stream);
                    }
                }
                if (MainBanner != null)
                {
                    if (restaurant.MainBanner != null)
                    {
                        if (MainBanner.Length > 20485760)
                        {
                            ModelState.AddModelError("MainBanner", "حجم عکس بیشتر از 2 مگابایات است");
                            return await Task.FromResult(RedirectToAction(nameof(Index)));
                        }
                        else if (!MainBanner.IsImage())
                        {
                            ModelState.AddModelError("MainBanner", "عکس معتبر نمی باشد");
                            return await Task.FromResult(RedirectToAction(nameof(Index)));
                        }
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Restaurant/", restaurant.MainBanner);
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    restaurant.MainBanner = Guid.NewGuid().ToString() + Path.GetExtension(MainBanner.FileName);
                    string savePath = Path.Combine(
                       Directory.GetCurrentDirectory(), "wwwroot/Images/Restaurant/", restaurant.MainBanner
                   );
                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await MainBanner.CopyToAsync(stream);
                    }
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
                updateRestaurant.UserId = restaurant.UserId;
                db.Restaurant.Update(updateRestaurant);
                db.Restaurant.Save();
                return await Task.FromResult(RedirectToAction(nameof(Index)));
            }
            return await Task.FromResult(View(restaurant));
        }
        public async Task<string> Delete(int id)
        {
            TblRestaurant selectedRestaurantById = db.Restaurant.GetById(id);
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
            return await Task.FromResult("خطا در حذف   لطفا بررسی فرمایید");
        }

        public ActionResult Gallery(int id)
        {
            try
            {
                ViewBag.Galleries = db.Image.Get().Where(p => p.RestaurantId == id).OrderByDescending(i => i.ImageId).ToList();
                return View(new TblImage()
                {
                    RestaurantId = id
                });
            }
            catch
            {
                return RedirectToAction("/ErrorPage/NotFound");
            }
        }

        [HttpPost]
        public async Task<IActionResult> GalleryAsync(TblImage galleries, IFormFile imgUp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (imgUp == null)
                    {
                        ModelState.AddModelError("Url", "عکس خالیست");

                    }
                    else
                    {
                        if (imgUp != null)
                        {
                            galleries.Url = Guid.NewGuid().ToString() + Path.GetExtension(imgUp.FileName);
                            string savePath = Path.Combine(
                                Directory.GetCurrentDirectory(), "wwwroot/Images/Restaurant/Gallery/", galleries.Url
                            );
                            using (var stream = new FileStream(savePath, FileMode.Create))
                            {
                                await imgUp.CopyToAsync(stream);
                            }
                        }
                        galleries.IsValid = true;
                        db.Image.Add(galleries);
                        db.Image.Save();
                        return RedirectToAction("Gallery", new { id = galleries.RestaurantId });
                    }
                }
                ViewBag.Galleries = db.Image.Get().Where(p => p.RestaurantId == galleries.RestaurantId).ToList();
                return await Task.FromResult(View(galleries));

            }
            catch
            {
                return RedirectToAction("/ErrorPage/NotFound");
            }
        }

        public IActionResult ShowHideImage(int id)
        {
            TblImage updateImage = db.Image.GetById(id);
            updateImage.IsValid = !updateImage.IsValid;
            db.Image.Update(updateImage);
            db.Image.Save();
            return RedirectToAction("Gallery", new { id = updateImage.RestaurantId });
        }
        public async Task<string> DeleteGallery(int id)
        {
            TblImage selectedImageById = db.Image.GetById(id);
            bool delete = db.Image.Delete(selectedImageById);
            if (delete)
            {
                if (selectedImageById.Url != null)
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Restaurant/Gallery/", selectedImageById.Url);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }
                db.Image.Save();
                return await Task.FromResult("true");
            }
            return await Task.FromResult("خطا در حذف   لطفا بررسی فرمایید");
        }

        public async Task<IActionResult> Food()
        {
            return await Task.FromResult(View());
        }
        public async Task<IActionResult> FoodType()
        {
            return await Task.FromResult(View());
        }
        public async Task<IActionResult> MealType()
        {
            return await Task.FromResult(View());
        }
        public async Task<IActionResult> Property()
        {
            return await Task.FromResult(View());
        }

        public async Task<IActionResult> Report(int restaurantId = 0, string name = null)
        {
            return await Task.FromResult(View());
        }
        public async Task<IActionResult> ReportSearch(int restaurantId = 0, string name = null)
        {
            List<TblReport> list = db.Report.Get().ToList();
            if (restaurantId > 0)
            {
                list = list.Where(i => i.Restaurant.RestaurantId == restaurantId).ToList();
            }
            if (name != null)
            {
                list = list.Where(i => i.Restaurant.Name.Contains(name)).ToList();
            }
            return await Task.FromResult(PartialView(list));

        }

        public async Task<string> DeleteReport(int id)
        {
            TblReport selectedCityById = db.Report.GetById(id);
            bool delete = db.Report.Delete(selectedCityById);
            if (delete)
            {
                db.Report.Save();
                return await Task.FromResult("true");
            }
            return await Task.FromResult("خطا در حذف   لطفا بررسی فرمایید");
        }

        public async Task<IActionResult> Search(string name = null, string tellNo = null, string cat = null, string user = null)
        {
            List<TblRestaurant> list = db.Restaurant.Get().ToList();
            if (name != null)
            {
                list = list.Where(i => i.Name.Contains(name)).ToList();
            }
            if (tellNo != null)
            {
                list = list.Where(i => i.TellNo1.Contains(tellNo) || i.TellNo2.Contains(tellNo)).ToList();
            }
            if (user != null)
            {
                list = list.Where(i => i.User.Name.Contains(user) || i.User.TellNo.Contains(user)).ToList();
            }
            if (cat != null)
            {
                list = list.Where(i => i.Catagory.Name.Contains(cat)).ToList();
            }
            return await Task.FromResult(PartialView(list));
        }


        public async Task<string> ActiveDisableRestauran(int id)
        {
            TblRestaurant updateUser = db.Restaurant.GetById(id);
            updateUser.IsValid = !updateUser.IsValid;
            db.Restaurant.Update(updateUser);
            db.Restaurant.Save();
            return await Task.FromResult("true");
        }

        public IActionResult ListRestauran()
        {
            return ViewComponent("RestauranListInAdminVm");
        }
    }
}
