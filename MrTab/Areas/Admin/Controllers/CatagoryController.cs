using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MrTab.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CatagoryController : Controller
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
        public async Task<IActionResult> CreateAsync(TblCatagory cat, IFormFile ImageUrl,IFormFile IconUrl)
        {
            if (ModelState.IsValid)
            {
                if (ImageUrl != null)
                {
                    cat.ImageUrl = Guid.NewGuid().ToString() + Path.GetExtension(ImageUrl.FileName);
                    string savePath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/Images/Catagory/", cat.ImageUrl
                    );
                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await ImageUrl.CopyToAsync(stream);
                    }
                }
                if (IconUrl != null)
                {
                    cat.IconUrl = Guid.NewGuid().ToString() + Path.GetExtension(IconUrl.FileName);
                    string savePath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/Images/Catagory/Icon/", cat.IconUrl
                    );
                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await IconUrl.CopyToAsync(stream);
                    }
                }
                TblCatagory addCat = new TblCatagory();
                addCat.Name = cat.Name;
                addCat.ImageUrl = cat.ImageUrl;
                addCat.IsHome = cat.IsHome;
                addCat.IconUrl = cat.IconUrl;
                db.Catagory.Add(addCat);
                db.Catagory.Save();
                return await Task.FromResult(RedirectToAction(nameof(Index)));
            }
            return await Task.FromResult(PartialView(cat));
        }
        public async Task<IActionResult> Edit(int id)
        {
            return await Task.FromResult(PartialView(db.Catagory.GetById(id)));
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(TblCatagory cat, IFormFile ImageUrl, IFormFile IconUrl)
        {
            if (ModelState.IsValid)
            {
                if (ImageUrl != null)
                {
                    if (cat.ImageUrl == null)
                    {
                        cat.ImageUrl = Guid.NewGuid().ToString() + Path.GetExtension(ImageUrl.FileName);
                    }
                    string savePath = Path.Combine(
                       Directory.GetCurrentDirectory(), "wwwroot/Images/Catagory/", cat.ImageUrl
                   );
                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await ImageUrl.CopyToAsync(stream);
                    }
                }
                if (IconUrl != null)
                {
                    cat.IconUrl = Guid.NewGuid().ToString() + Path.GetExtension(IconUrl.FileName);
                    string savePath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/Images/Catagory/Icon/", cat.IconUrl
                    );
                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await IconUrl.CopyToAsync(stream);
                    }
                }
                TblCatagory editCat = db.Catagory.GetById(cat.CatagoryId);
                editCat.Name = cat.Name;
                editCat.ImageUrl = cat.ImageUrl;
                editCat.IsHome = cat.IsHome;
                editCat.IconUrl = cat.IconUrl;
                db.Catagory.Update(editCat);
                db.Catagory.Save();
                return await Task.FromResult(RedirectToAction(nameof(Index)));
            }
            return await Task.FromResult(PartialView(cat));
        }
        public async Task<string> Delete(int id)
        {
            TblCatagory selectedCatById = db.Catagory.GetById(id);
            if (db.Restaurant.Get().Any(i => i.CatagoryId == id))
            {
                return await Task.FromResult("خطا در حذف   لطفا بررسی فرمایید");
            }
            bool delete = db.Catagory.Delete(selectedCatById);
            if (delete)
            {
                if (selectedCatById.ImageUrl != null)
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Catagory/", selectedCatById.ImageUrl);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }
                if (selectedCatById.IconUrl != null)
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Catagory/Icon/", selectedCatById.IconUrl);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }
                db.Catagory.Save();
                return await Task.FromResult("true");
            }
            return await Task.FromResult("خطا در حذف   لطفا بررسی فرمایید");
        }


        public async Task<IActionResult> Search(string name = null)
        {
            List<TblCatagory> list = db.Catagory.Get().ToList();
            if (name != null)
            {
                list = list.Where(i => i.Name.Contains(name)).ToList();
            }
            return await Task.FromResult(PartialView(list));
        }

    }
}
