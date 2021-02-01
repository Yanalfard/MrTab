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
    public class CommentVideoReportController : Controller
    {
        private Core db = new Core();
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Search(int restaurantId = 0, string name = null, string user = null)
        {
            List<TblComment> list = db.Comment.Get(i => i.DocId != null && i.IsReported == true).ToList();
            if (restaurantId > 0)
            {
                list = list.Where(i => i.Doc.DocId == restaurantId).ToList();
            }
            if (name != null)
            {
                list = list.Where(i => i.Doc.Title.Contains(name)).ToList();
            }
            if (user != null)
            {
                list = list.Where(i => i.User.Name.Contains(user)).ToList();
            }
            return await Task.FromResult(PartialView(list));
        }

        public async Task<string> DeleteCommentReport(int id)
        {
            TblComment updateComment = db.Comment.GetById(id);
            updateComment.IsReported = !updateComment.IsReported;
            db.Comment.Update(updateComment);
            db.Comment.Save();
            return await Task.FromResult("true");
        }
        public async Task<string> ActiveDisableCommentReport(int id)
        {
            TblComment updateComment = db.Comment.GetById(id);
            updateComment.IsValid = !updateComment.IsValid;
            db.Comment.Update(updateComment);
            db.Comment.Save();
            return await Task.FromResult("true");
        }

        public IActionResult ListCommentReport()
        {
            return ViewComponent("CommentVideoReportListInAdminVm");
        }
    }
}
