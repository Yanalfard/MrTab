using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MrTab.Utilities;
using Services.Services;

namespace MrTab.Controllers
{
    public class RestaurantController : Controller
    {
        private Core db = new Core();

        TblUser SelectUser()
        {
            int userId = Convert.ToInt32(User.Claims.First().Value);
            TblUser selectUser = db.User.GetById(userId);
            return selectUser;
        }
        [Route("ViewSingle/{id}/{name}")]
        public IActionResult ViewSingle(int id, string name)
        {
            return View(db.Restaurant.GetById(id));
        }


        public async Task<IActionResult> UploadImage(int id)
        {
            return await Task.FromResult(ViewComponent("UploadImageRestaurantVm", new { id = id }));
        }
        [HttpPost]
        public async Task<IActionResult> UploadImageAsync(UploadImageRestaurant uploadImage, IFormFile files)
        {
            TblRestaurant select = db.Restaurant.GetById(uploadImage.id);
            if (files != null)
            {
                uploadImage.Image = Guid.NewGuid().ToString() + Path.GetExtension(files.FileName);
                string savePath = Path.Combine(
                    Directory.GetCurrentDirectory(), "wwwroot/Images/Restaurant/Gallery/", uploadImage.Image
                );
                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await files.CopyToAsync(stream);
                }
                TblImage addImage = new TblImage();
                addImage.RestaurantId = uploadImage.id;
                addImage.Url = uploadImage.Image;
                addImage.Status = 2;
                db.Image.Add(addImage);
                db.User.Save();
                return await Task.FromResult(Redirect("/ViewSingle/" + select.RestaurantId + "/" + select.Name.Replace(" ", "-")));
            }
            return await Task.FromResult(Redirect("/ViewSingle/" + select.RestaurantId + "/" + select.Name.Replace(" ", "-")));

        }

        public async Task<IActionResult> ReportRestaurant(int id)
        {
            return await Task.FromResult(ViewComponent("ReportRestaurantVm", new { id = id }));
        }
        [HttpPost]
        public async Task<IActionResult> ReportRestaurant(TblReport report)
        {
            TblRestaurant select = db.Restaurant.GetById(report.RestaurantId);
            db.Report.Add(report);
            db.Report.Save();
            return await Task.FromResult(Redirect("/ViewSingle/" + select.RestaurantId + "/" + select.Name.Replace(" ", "-")));

        }

        public async Task<IActionResult> SendCommentRestaurant(int id)
        {
            return await Task.FromResult(ViewComponent("SendCommentRestaurantVm", new { id = id }));
        }
        [HttpPost]
        [PermissionChecker("user")]
        public async Task<IActionResult> SendCommentRestaurant(SendCommentVm sendComment)
        {
            TblRestaurant select = db.Restaurant.GetById(sendComment.RestaurantId);
            if (select.RateCount == 0)
            {
                select.DecorRate = sendComment.DecorRate;
                select.ServiceRate = sendComment.ServiceRate;
                select.QualityPerPriceRate = sendComment.QualityPerPriceRate;
                select.QualityRate = sendComment.QualityRate;
                select.RateCount = 1;
            }
            else
            {
                var DecorRate = select.RateCount * select.DecorRate;
                var ServiceRate = select.RateCount * select.ServiceRate;
                var QualityPerPriceRate = select.RateCount * select.QualityPerPriceRate;
                var QualityRate = select.RateCount * select.QualityRate;

                select.RateCount++;
                select.DecorRate = (short)((DecorRate + sendComment.DecorRate) / select.RateCount);
                select.ServiceRate = (short)((ServiceRate + sendComment.ServiceRate) / select.RateCount);
                select.QualityPerPriceRate = (short)((QualityPerPriceRate + sendComment.QualityPerPriceRate) / select.RateCount);
                select.QualityRate = (short)((QualityRate + sendComment.QualityRate) / select.RateCount);
            }
            var rat = (select.DecorRate + select.ServiceRate + select.QualityPerPriceRate + select.QualityRate) / 4;
            select.Rating = (short)(rat * 10);
            TblComment addComment = new TblComment();
            addComment.Text = sendComment.Comment;
            addComment.RestaurantId = sendComment.RestaurantId;
            addComment.UserId = SelectUser().UserId;
            addComment.DateSubmited = DateTime.Now;
            addComment.LikeCount = 0;
            addComment.IsReported = false;
            addComment.DecorRate = sendComment.DecorRate;
            addComment.ServiceRate = sendComment.ServiceRate;
            addComment.QualityPerPriceRate = sendComment.QualityPerPriceRate;
            addComment.QualityRate = sendComment.QualityRate;
            addComment.Rate = 0;
            addComment.ExpenseRate = 0;
            var c = db.Comment.Add(addComment);
            db.Restaurant.Update(select);
            db.Restaurant.Save();
            return await Task.FromResult(Redirect("/ViewSingle/" + select.RestaurantId + "/" + select.Name.Replace(" ", "-")));

        }

        public async Task<IActionResult> ReportCommentRestaurant(int RestaurantId, int CommentId)
        {
            return await Task.FromResult(ViewComponent("ReportCommentRestaurantVm", new { RestaurantId = RestaurantId, CommentId = CommentId }));
        }
        [HttpPost]
        public async Task<IActionResult> ReportCommentRestaurant(ReportCommentRestaurant report)
        {
            TblRestaurant select = db.Restaurant.GetById(report.RestaurantId);
            TblComment selectComment = db.Comment.GetById(report.CommentId);
            selectComment.TextReport = report.Text + report.Description;
            selectComment.IsReported = true;
            db.Comment.Update(selectComment);
            db.Comment.Save();
            return await Task.FromResult(Redirect("/ViewSingle/" + select.RestaurantId + "/" + select.Name.Replace(" ", "-")));

        }
    }
}
