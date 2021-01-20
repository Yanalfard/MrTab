using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrTab.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MealTypeController : Controller
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
            return await Task.FromResult(PartialView(new TblMealType()
            {
                RestaurantId = id
            }));
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(TblMealType mealType)
        {
            if (ModelState.IsValid)
            {
                db.MealType.Add(mealType);
                db.MealType.Save();
                return await Task.FromResult(Redirect("/Admin/MealType?id=" + mealType.RestaurantId));
            }
            return await Task.FromResult(PartialView(mealType));
        }
        public async Task<IActionResult> Edit(int id)
        {
            return await Task.FromResult(PartialView(db.MealType.GetById(id)));
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(TblMealType mealType)
        {
            if (ModelState.IsValid)
            {
                TblMealType editFoodType = db.MealType.GetById(mealType.MealTypeId);
                editFoodType.Name = mealType.Name;
                db.MealType.Update(editFoodType);
                db.MealType.Save();
                return await Task.FromResult(Redirect("/Admin/MealType?id=" + mealType.RestaurantId));
            }
            return await Task.FromResult(PartialView(mealType));
        }

        public async Task<string> Delete(int id)
        {
            TblMealType selectedMealTypeById = db.MealType.GetById(id);
            bool delete = db.MealType.Delete(selectedMealTypeById);
            if (delete)
            {
                db.MealType.Save();
                return await Task.FromResult("true");
            }
            return await Task.FromResult("خطا در حذف   لطفا بررسی فرمایید");
        }

        public async Task<IActionResult> Search(string name = null)
        {
            List<TblMealType> list = db.MealType.Get().ToList();
            if (name != null)
            {
                list = list.Where(i => i.Name.Contains(name)).ToList();
            }
            return await Task.FromResult(PartialView(list));
        }
    }
}
