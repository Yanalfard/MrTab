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
    public class FoodController : Controller
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
            return await Task.FromResult(PartialView(new TblFood()
            {
                RestaurantId = id
            }));
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(TblFood food)
        {
            if (ModelState.IsValid)
            {
                db.Food.Add(food);
                db.Food.Save();
                //return await Task.FromResult(Redirect("/ADminIndex" + food.RestaurantId));

                return await Task.FromResult(Redirect("/Admin/Food?id=" + food.RestaurantId));
            }
            return await Task.FromResult(PartialView(food));
        }
        public async Task<IActionResult> Edit(int id)
        {
            return await Task.FromResult(PartialView(db.Food.GetById(id)));
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(TblFood food)
        {
            if (ModelState.IsValid)
            {
                TblFood editFood = db.Food.GetById(food.FoodId);
                editFood.Name = food.Name;
                db.Food.Update(editFood);
                db.Food.Save();
                return await Task.FromResult(Redirect("/Admin/Food?id=" + food.RestaurantId));
            }
            return await Task.FromResult(PartialView(food));
        }

        public async Task<string> Delete(int id)
        {
            TblFood selectedFoodById = db.Food.GetById(id);
            bool delete = db.Food.Delete(selectedFoodById);
            if (delete)
            {
                db.Food.Save();
                return await Task.FromResult("true");
            }
            return await Task.FromResult("خطا در حذف   لطفا بررسی فرمایید");
        }
        public async Task<IActionResult> Search(string name = null)
        {
            List<TblFood> list = db.Food.Get().ToList();
            if (name != null)
            {
                list = list.Where(i => i.Name.Contains(name)).ToList();
            }
            return await Task.FromResult(PartialView(list));
        }
    }
}
