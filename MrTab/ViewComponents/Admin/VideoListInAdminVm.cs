using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrTab.ViewComponents.Admin
{
    public class VideoListInAdminVm : ViewComponent
    {
        private Core db = new Core();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var list = db.Restaurant.Get();
            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Video/Components/VideoListInAdmin.cshtml", db.Doc.Get().OrderByDescending(i => i.DocId)));
        }
    }
}
