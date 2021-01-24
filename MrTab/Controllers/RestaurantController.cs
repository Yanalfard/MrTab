using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace MrTab.Controllers
{
    public class RestaurantController : Controller
    {
        private Core db = new Core();
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
    }
}
