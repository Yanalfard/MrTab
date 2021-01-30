using DataLayer.Metadata;
using DataLayer.Models;
using DataLayer.ViewModel;
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
    [PermissionChecker("admin")]
    public class VideoController : Controller
    {
        private Core db = new Core();

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            return await Task.FromResult(View());
        }
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 104857600)]
        public async Task<IActionResult> CreateAsync(DocVm video, List<IFormFile> MainImage, IFormFile VideoUrl)
        {
            if (ModelState.IsValid)
            {
                if (VideoUrl == null)
                {
                    ModelState.AddModelError("VideoUrl", "ویدیو خالیست");
                }
                else if (MainImage.Count() == 0)
                {
                    ModelState.AddModelError("MainImage1", "حداقل یک عکس الزامیست");
                }
                else
                {
                    if (MainImage.Count() != 0)
                    {
                        for (int i = 0; i <= MainImage.Count; i++)
                        {
                            string savePath;
                            if (i == 0)
                            {
                                video.MainImage1 = Guid.NewGuid().ToString() + Path.GetExtension(MainImage[i].FileName);
                                savePath = Path.Combine(
                                    Directory.GetCurrentDirectory(), "wwwroot/Images/Video/Images/", video.MainImage1
                                );
                                using (var stream = new FileStream(savePath, FileMode.Create))
                                {
                                    await MainImage[i].CopyToAsync(stream);
                                }
                            }
                            else if (i == 1)
                            {
                                video.MainImage2 = Guid.NewGuid().ToString() + Path.GetExtension(MainImage[i].FileName);
                                savePath = Path.Combine(
                                    Directory.GetCurrentDirectory(), "wwwroot/Images/Video/Images/", video.MainImage2
                                );
                                using (var stream = new FileStream(savePath, FileMode.Create))
                                {
                                    await MainImage[i].CopyToAsync(stream);
                                }
                            }
                            else if (i == 2)
                            {
                                video.MainImage3 = Guid.NewGuid().ToString() + Path.GetExtension(MainImage[i].FileName);
                                savePath = Path.Combine(
                                    Directory.GetCurrentDirectory(), "wwwroot/Images/Video/Images/", video.MainImage3
                                );
                                using (var stream = new FileStream(savePath, FileMode.Create))
                                {
                                    await MainImage[i].CopyToAsync(stream);
                                }
                            }

                        }
                    }
                    if (VideoUrl != null)
                    {
                        video.VideoUrl = Guid.NewGuid().ToString() + Path.GetExtension(VideoUrl.FileName);
                        string savePath = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot/Images/Video/Videos/", video.VideoUrl
                        );
                        using (var stream = new FileStream(savePath, FileMode.Create))
                        {
                            await VideoUrl.CopyToAsync(stream);
                        }
                    }
                    TblDoc addDoc = new TblDoc();
                    addDoc.Title = video.Title;
                    addDoc.BodyHtml = video.BodyHtml;
                    addDoc.MainImage1 = video.MainImage1;
                    addDoc.MainImage2 = video.MainImage2;
                    addDoc.MainImage3 = video.MainImage3;
                    addDoc.VideoUrl = video.VideoUrl;
                    addDoc.LikeCount = 0;
                    db.Doc.Add(addDoc);
                    db.Doc.Save();
                    return await Task.FromResult(RedirectToAction(nameof(Index)));
                }
            }
            return await Task.FromResult(View(video));
        }
        public async Task<IActionResult> Edit(int id)
        {
            TblDoc selectedDoc = db.Doc.GetById(id);
            DocVm md = new DocVm();
            md.DocId = selectedDoc.DocId;
            md.BodyHtml = selectedDoc.BodyHtml;
            md.Title = selectedDoc.Title;
            md.VideoUrl = selectedDoc.VideoUrl;
            md.MainImage1 = selectedDoc.MainImage1;
            md.MainImage2 = selectedDoc.MainImage2;
            md.MainImage3 = selectedDoc.MainImage3;
            md.LikeCount = selectedDoc.LikeCount;
            return await Task.FromResult(View(md));
        }
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 104857600)]
        public async Task<IActionResult> EditAsync(DocVm video, IFormFile MainImage1, IFormFile MainImage2, IFormFile MainImage3, IFormFile VideoUrl)
        {
            if (ModelState.IsValid)
            {
                if (MainImage1 != null)
                {
                    if (video.MainImage1 != null)
                    {
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Video/Images/", video.MainImage1);
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    video.MainImage1 = Guid.NewGuid().ToString() + Path.GetExtension(MainImage1.FileName);
                    string savePath = Path.Combine(
                       Directory.GetCurrentDirectory(), "wwwroot/Images/Video/Images/", video.MainImage1
                   );
                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await MainImage1.CopyToAsync(stream);
                    }
                }
                if (MainImage2 != null)
                {
                    if (video.MainImage2 != null)
                    {
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Video/Images/", video.MainImage2);
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    video.MainImage2 = Guid.NewGuid().ToString() + Path.GetExtension(MainImage2.FileName);
                    string savePath = Path.Combine(
                       Directory.GetCurrentDirectory(), "wwwroot/Images/Video/Images/", video.MainImage2
                   );
                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await MainImage2.CopyToAsync(stream);
                    }
                }
                if (MainImage3 != null)
                {
                    if (video.MainImage3 != null)
                    {
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Video/Images/", video.MainImage3);
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    video.MainImage3 = Guid.NewGuid().ToString() + Path.GetExtension(MainImage3.FileName);
                    string savePath = Path.Combine(
                       Directory.GetCurrentDirectory(), "wwwroot/Images/Video/Images/", video.MainImage3
                   );
                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await MainImage3.CopyToAsync(stream);
                    }
                }
                if (VideoUrl != null)
                {
                    if (video.VideoUrl != null)
                    {
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Video/Videos/", video.VideoUrl);
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    video.VideoUrl = Guid.NewGuid().ToString() + Path.GetExtension(VideoUrl.FileName);
                    string savePath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/Images/Video/Videos/", video.VideoUrl
                    );
                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await VideoUrl.CopyToAsync(stream);
                    }
                }
                TblDoc updateDoc = db.Doc.GetById(video.DocId);
                updateDoc.Title = video.Title;
                updateDoc.BodyHtml = video.BodyHtml;
                updateDoc.VideoUrl = video.VideoUrl;
                updateDoc.MainImage1 = video.MainImage1;
                updateDoc.MainImage1 = video.MainImage2;
                updateDoc.MainImage3 = video.MainImage3;
                updateDoc.LikeCount = video.LikeCount;
                db.Doc.Update(updateDoc);
                db.Doc.Save();
                return await Task.FromResult(RedirectToAction(nameof(Index)));
            }
            return await Task.FromResult(View(video));
        }
        public async Task<string> Delete(int id)
        {
            TblDoc selectedDocById = db.Doc.GetById(id);
            bool delete = db.Doc.Delete(selectedDocById);
            if (delete)
            {
                if (selectedDocById.MainImage1 != null)
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Vidoe/Images/", selectedDocById.MainImage1);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }
                if (selectedDocById.MainImage2 != null)
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Vidoe/Images/", selectedDocById.MainImage2);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }
                if (selectedDocById.MainImage3 != null)
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Vidoe/Images/", selectedDocById.MainImage3);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }
                if (selectedDocById.VideoUrl != null)
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Vidoe/Videos/", selectedDocById.VideoUrl);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                db.Doc.Save();
                return await Task.FromResult("true");
            }
            return await Task.FromResult("خطا در حذف   لطفا بررسی فرمایید");
        }

        public async Task<IActionResult> Search(string name = null)
        {
            List<TblDoc> list = db.Doc.Get().ToList();
            if (name != null)
            {
                list = list.Where(i => i.Title.Contains(name)).ToList();
            }
            return await Task.FromResult(PartialView(list));
        }

    }
}
