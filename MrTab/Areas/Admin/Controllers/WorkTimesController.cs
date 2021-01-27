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
    public class WorkTimesController : Controller
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
            return await Task.FromResult(PartialView(new TblWorkTime()
            {
                RestaurantId = id
            }));
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(TblWorkTime workTime)
        {
            if (ModelState.IsValid)
            {
                db.WorkTime.Add(workTime);
                db.WorkTime.Save();
                return await Task.FromResult(Redirect("/Admin/WorkTimes?id=" + workTime.RestaurantId));
            }
            return await Task.FromResult(PartialView(workTime));
        }
        public async Task<IActionResult> Edit(int id)
        {
            return await Task.FromResult(PartialView(db.WorkTime.GetById(id)));
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(TblWorkTime workTime)
        {
            if (ModelState.IsValid)
            {
                TblWorkTime editWorkTime = db.WorkTime.GetById(workTime.WorkTimeId);
                editWorkTime.Day = workTime.Day;
                editWorkTime.Time = workTime.Time;
                db.WorkTime.Update(editWorkTime);
                db.WorkTime.Save();
                return await Task.FromResult(Redirect("/Admin/WorkTimes?id=" + workTime.RestaurantId));
            }
            return await Task.FromResult(PartialView(workTime));
        }

        public async Task<string> Delete(int id)
        {
            TblWorkTime selectedWorkTimeById = db.WorkTime.GetById(id);
            bool delete = db.WorkTime.Delete(selectedWorkTimeById);
            if (delete)
            {
                db.WorkTime.Save();
                return await Task.FromResult("true");
            }
            return await Task.FromResult("خطا در حذف   لطفا بررسی فرمایید");
        }
        public async Task<IActionResult> Search(string name = null)
        {
            List<TblWorkTime> list = db.WorkTime.Get().ToList();
            if (name != null)
            {
                list = list.Where(i => i.Day.Contains(name)).ToList();
            }
            return await Task.FromResult(PartialView(list));
        }
    }
}
