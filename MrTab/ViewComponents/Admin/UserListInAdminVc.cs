using DataLayer.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrTab.ViewComponents.Admin
{
    public class UserListInAdminVc: ViewComponent
    {
        private Core db = new Core();
        public async Task<IViewComponentResult> InvokeAsync(UserSeachVm search)
        {
            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/User/Components/UserListInAdmin.cshtml", db.User.Get()));
        }
    }
}
