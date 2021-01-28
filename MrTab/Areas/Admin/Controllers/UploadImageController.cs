using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MrTab.Areas.Admin.Controllers
{
    public class UploadImageController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> UploadImages(IFormFile upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            try
            {
                string vImagePath = String.Empty;
                string vMessage = String.Empty;
                string vFilePath = String.Empty;
                string vOutput = String.Empty;
                try
                {
                    if (upload != null)
                    {
                        if (upload != null && upload.Length > 0)
                        {
                            var vFileName = Guid.NewGuid().ToString() + Path.GetExtension(upload.FileName);
                            string savePath = Path.Combine(
                               Directory.GetCurrentDirectory(), "wwwroot/Images/UploadEditor/", vFileName
                           );
                            using (var stream = new FileStream(savePath, FileMode.Create))
                            {
                                await upload.CopyToAsync(stream);
                            }
                            vImagePath = Url.Content("/Images/UploadEditor/" + vFileName);
                            vMessage = "عکس مورد نظر آپلود شد";
                        }
                    }


                    ///wwwroot/Images/UploadEditor/
                }
                catch
                {
                    vMessage = "There was an issue uploading";
                }
                vOutput = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + vImagePath + "\", \"" + vMessage + "\");</script></body></html>";
                return Content(vOutput);
            }
            catch
            {
                return RedirectToAction("/ErrorPage/NotFound");
            }
        }

    }
}
