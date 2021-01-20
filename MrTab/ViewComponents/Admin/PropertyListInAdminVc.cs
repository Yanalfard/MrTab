using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrTab.ViewComponents.Admin
{
    public class PropertyListInAdminVc : ViewComponent
    {
        private Core db = new Core();
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            List<TblProperty> selectedPropertyByResId = db.Property.Get().Where(i => i.RestaurantId == id).ToList();
            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Property/Components/PropertyListInAdmin.cshtml", selectedPropertyByResId));
        }
    }
}
