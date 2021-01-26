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
    public class CommentController : Controller
    {
        private Core db = new Core();
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Search(int restaurantId = 0, string name = null, string user = null)
        {
            List<TblComment> list = db.Comment.Get().ToList();
            if (restaurantId > 0)
            {
                list = list.Where(i => i.Restaurant.RestaurantId == restaurantId).ToList();
            }
            if (name != null)
            {
                list = list.Where(i => i.Restaurant.Name.Contains(name)).ToList();
            }
            if (user != null)
            {
                list = list.Where(i => i.User.Name.Contains(user)).ToList();
            }
            return await Task.FromResult(PartialView(list));
        }

        public async Task<string> DeleteComment(int id)
        {
            TblComment selectedCityById = db.Comment.GetById(id);
            bool delete = db.Comment.Delete(selectedCityById);
            if (delete)
            {
                db.Report.Save();
                return await Task.FromResult("true");
            }
            return await Task.FromResult("خطا در حذف   لطفا بررسی فرمایید");
        }
        public async Task<string> ActiveDisableComment(int id)
        {
            TblComment updateComment = db.Comment.GetById(id);
            updateComment.IsValid = !updateComment.IsValid;
            db.Comment.Update(updateComment);
            db.Comment.Save();
            return await Task.FromResult("true");
        }

        public IActionResult ListComment()
        {
            return ViewComponent("CommentListInAdminVm");
        }

    }
}
