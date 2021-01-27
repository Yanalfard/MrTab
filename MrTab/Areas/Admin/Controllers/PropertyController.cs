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
    [PermissionChecker("author,admin")]
    public class PropertyController : Controller
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
            return await Task.FromResult(PartialView(new TblProperty()
            {
                RestaurantId = id
            }));
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(TblProperty property)
        {
            if (ModelState.IsValid)
            {
                db.Property.Add(property);
                db.Property.Save();
                return await Task.FromResult(Redirect("/Admin/Property?id=" + property.RestaurantId));
            }
            return await Task.FromResult(PartialView(property));
        }
        public async Task<IActionResult> Edit(int id)
        {
            return await Task.FromResult(PartialView(db.Property.GetById(id)));
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(TblProperty property)
        {
            if (ModelState.IsValid)
            {
                TblProperty editProperty = db.Property.GetById(property.PropertyId);
                editProperty.Name = property.Name;
                db.Property.Update(editProperty);
                db.Property.Save();
                return await Task.FromResult(Redirect("/Admin/Property?id=" + property.RestaurantId));
            }
            return await Task.FromResult(PartialView(property));
        }

        public async Task<string> Delete(int id)
        {
            TblProperty selectedPropertyById = db.Property.GetById(id);
            bool delete = db.Property.Delete(selectedPropertyById);
            if (delete)
            {
                db.Property.Save();
                return await Task.FromResult("true");
            }
            return await Task.FromResult("خطا در حذف   لطفا بررسی فرمایید");
        }
        public async Task<IActionResult> Search(string name = null)
        {
            List<TblProperty> list = db.Property.Get().ToList();
            if (name != null)
            {
                list = list.Where(i => i.Name.Contains(name)).ToList();
            }
            return await Task.FromResult(PartialView(list));
        }
    }
}
