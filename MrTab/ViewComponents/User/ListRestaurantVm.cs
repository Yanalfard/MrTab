using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrTab.ViewComponents.User
{
    public class ListRestaurantVm : ViewComponent
    {
        private Core db = new Core();
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            int userId = Convert.ToInt32(id);
            List<TblRestaurant> selectRestaurant = db.Restaurant.Get().Where(i => i.UserId == userId).ToList();
            return await Task.FromResult((IViewComponentResult)View("/Areas/User/Views/Account/Components/ListRestaurant.cshtml", selectRestaurant));
        }
    }
}
