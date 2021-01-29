using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrTab.ViewComponents.Admin
{
    public class RestauranIsValidListInAdminVm : ViewComponent
    {
        private Core db = new Core();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var list = db.Restaurant.Get();
            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Restauran/Components/RestauranIsValidListInAdmin.cshtml", db.Restaurant.Get(i=>i.IsValid==false).OrderByDescending(i => i.RestaurantId)));
        }
    }
}
