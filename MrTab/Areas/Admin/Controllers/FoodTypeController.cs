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
    [PermissionChecker("admin")]
    public class FoodTypeController : Controller
    {
        private Core db = new Core();
        public async Task<IActionResult> Index(int id, string name = null)
        {
            ViewBag.name = name;
            ViewBag.Id = id;
            if (id != 0)
            {
                ViewData["Title"] = db.Restaurant.GetById(id).Name;
            }
            return await Task.FromResult(View());
        }
        public async Task<IActionResult> Create(int id)
        {
            return await Task.FromResult(PartialView(new TblFoodType()
            {
                RestaurantId = id
            }));
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(TblFoodType foodType)
        {
            if (ModelState.IsValid)
            {
                db.FoodType.Add(foodType);
                db.FoodType.Save();
                return await Task.FromResult(Redirect("/Admin/FoodType?id=" + foodType.RestaurantId));
            }
            return await Task.FromResult(PartialView(foodType));
        }
        public async Task<IActionResult> Edit(int id)
        {
            return await Task.FromResult(PartialView(db.FoodType.GetById(id)));
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(TblFoodType foodType)
        {
            if (ModelState.IsValid)
            {
                TblFoodType editFoodType = db.FoodType.GetById(foodType.FoodTypeId);
                editFoodType.Name = foodType.Name;
                db.FoodType.Update(editFoodType);
                db.FoodType.Save();
                return await Task.FromResult(Redirect("/Admin/FoodType?id=" + foodType.RestaurantId));
            }
            return await Task.FromResult(PartialView(foodType));
        }

        public async Task<string> Delete(int id)
        {
            TblFoodType selectedFoodTypeById = db.FoodType.GetById(id);
            bool delete = db.FoodType.Delete(selectedFoodTypeById);
            if (delete)
            {
                db.FoodType.Save();
                return await Task.FromResult("true");
            }
            return await Task.FromResult("خطا در حذف   لطفا بررسی فرمایید");
        }
        public async Task<IActionResult> Search(string name = null)
        {
            List<TblFoodType> list = db.FoodType.Get().ToList();
            if (name != null)
            {
                list = list.Where(i => i.Name.Contains(name)).ToList();
            }
            return await Task.FromResult(PartialView(list));
        }
    }
}
