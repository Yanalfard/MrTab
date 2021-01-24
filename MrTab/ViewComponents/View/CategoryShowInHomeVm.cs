using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrTab.ViewComponents.View
{
    public class CategoryShowInHomeVm : ViewComponent
    {
        private Core db = new Core();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("/Views/Shared/Components/CategoryShowInHomeVm/CategoryShowInHome.cshtml", db.Catagory.Get().Where(i=>i.IsHome).OrderByDescending(i=>i.CatagoryId).Skip(1)));
        }
    }
}
