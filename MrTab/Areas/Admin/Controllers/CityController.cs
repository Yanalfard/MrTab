﻿using DataLayer.Metadata;
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
    public class CityController : Controller
    {
        private Core db = new Core();
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Create()
        {
            return await Task.FromResult(PartialView());
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(TblCity city)
        {
            if (ModelState.IsValid)
            {
                TblCity addCity = new TblCity();
                addCity.Name = city.Name;
                db.City.Add(addCity);
                db.City.Save();
                return await Task.FromResult(RedirectToAction(nameof(Index)));
            }
            return await Task.FromResult(PartialView(city));
        }
        public async Task<IActionResult> Edit(int id)
        {
            return await Task.FromResult(PartialView(db.City.GetById(id)));
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(TblCity city)
        {
            if (ModelState.IsValid)
            {
                TblCity editCity = db.City.GetById(city.CityId);
                editCity.Name = city.Name;
                db.City.Update(editCity);
                db.City.Save();
                return await Task.FromResult(RedirectToAction(nameof(Index)));
            }
            return await Task.FromResult(PartialView(city));
        }

        public async Task<string> Delete(int id)
        {
            TblCity selectedCityById = db.City.GetById(id);
            bool delete = db.City.Delete(selectedCityById);
            if (delete)
            {
                db.City.Save();
                return await Task.FromResult("true");
            }
            return await Task.FromResult("خطا در حذف   لطفا بررسی فرمایید");
        }
    }
}
