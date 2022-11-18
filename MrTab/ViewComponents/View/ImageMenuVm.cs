using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrTab.ViewComponents.View
{
    public class ImageMenuVm : ViewComponent
    {
        private Core db = new Core();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string imageMenuName = db.Config.Get().FirstOrDefault(i => i.Keyword.Contains("ImageMenu")).Value;
            return await Task.FromResult((IViewComponentResult)Content(imageMenuName));
        }
    }
}
