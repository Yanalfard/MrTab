﻿using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrTab.ViewComponents.View
{
    public class CategoryGridShowInHomeVm : ViewComponent
    {
        private Core db = new Core();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("/Views/Shared/Components/CategoryGridShowInHomeVm/CategoryGridShowInHome.cshtml", db.Catagory.Get().Where(i => i.IsHome).Where(i => i.ImageUrl != null).OrderByDescending(i => i.CatagoryId).Take(8)));
        }
    }
}