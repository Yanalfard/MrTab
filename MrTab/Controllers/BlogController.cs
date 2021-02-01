using DataLayer.Models;
using DataLayer.ViewModel;
using Microsoft.AspNetCore.Mvc;
using MrTab.Utilities;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrTab.Controllers
{
    public class BlogController : Controller
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
            return View();
        }
        [Route("Blog/{id}/{name}")]
        public async Task<IActionResult> ShowBlog(int id,string name)
        {
            TblDoc selectedBlog = db.Doc.GetById(id);
            return await Task.FromResult(View(selectedBlog));
        }
        [PermissionChecker("user")]
        public async Task<IActionResult> SendCommentVideo(TblComment sendComment)
        {
            try
            {
                TblDoc select = db.Doc.GetById(sendComment.DocId);
                TblComment addComment = new TblComment();
                addComment.Text = sendComment.Text;
                addComment.DocId = sendComment.DocId;
                addComment.UserId = SelectUser().UserId;
                addComment.DateSubmited = DateTime.Now;
                addComment.LikeCount = 0;
                addComment.IsReported = false;
                addComment.DecorRate = 0;
                addComment.ServiceRate = 0;
                addComment.QualityPerPriceRate = 0;
                addComment.QualityRate = 0;
                addComment.Rate = 0;
                addComment.ExpenseRate = 0;
                var c = db.Comment.Add(addComment);
                db.Comment.Save();
                return await Task.FromResult(Redirect("/Blog/" + select.DocId + "/" + select.Title.Replace(" ", "-")));
            }
            catch (Exception)
            {
                return await Task.FromResult(Redirect("/"));
            }
        }

        public async Task<IActionResult> ReportCommentBlog(int DocId, int CommentId)
        {
            return await Task.FromResult(ViewComponent("ReportCommentVideoVm", new { DocId = DocId, CommentId = CommentId }));
        }
        [HttpPost]
        public async Task<IActionResult> ReportCommentBlog(ReportCommentVideo report)
        {
            TblDoc select = db.Doc.GetById(report.DocId);
            TblComment addComment = new TblComment();
            TblComment selectComment = db.Comment.GetById(report.CommentId);
            selectComment.TextReport = report.Text + report.Description;
            selectComment.IsReported = true;
            db.Comment.Update(selectComment);
            db.Comment.Save();
            return await Task.FromResult(Redirect("/Blog/" + select.DocId + "/" + select.Title.Replace(" ", "-")));
        }
    }
}
