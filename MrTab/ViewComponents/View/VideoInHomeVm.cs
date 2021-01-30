using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrTab.ViewComponents.View
{
    public class VideoInHomeVm : ViewComponent
    {
        private Core db = new Core();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("/Views/Shared/Components/VideoInHomeVm/VideoInHome.cshtml", db.Doc.Get().OrderByDescending(i => i.DocId)));
        }
    }
}
