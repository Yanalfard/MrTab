using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrTab.ViewComponents.Admin
{
    public class MealTypeListInAdminVc : ViewComponent
    {
        private Core db = new Core();
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            List<TblMealType> selectedMealTypeByResId = db.MealType.Get().Where(i => i.RestaurantId == id).ToList();
            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/MealType/Components/MealTypeListInAdmin.cshtml", selectedMealTypeByResId));
        }
    }
}
