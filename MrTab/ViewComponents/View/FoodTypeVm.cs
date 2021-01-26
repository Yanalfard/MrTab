using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace MrTab.ViewComponents.View
{
    public class FoodTypeVm: ViewComponent
    {
        private Core db = new Core();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("/Views/Shared/Components/FoodTypeVm/FoodType.cshtml", db.FoodType.Get().OrderByDescending(i => i.FoodTypeId)));
        }
    }
}
