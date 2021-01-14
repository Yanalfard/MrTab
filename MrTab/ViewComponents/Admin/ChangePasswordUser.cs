using DataLayer.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrTab.ViewComponents.Admin
{
    public class ChangePasswordUser: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            VmChangePassword selectedId = new VmChangePassword();
            selectedId.Id = id;
            //return PartialView(new VmChangePassword()
            //{
            //    Id = id,
            //});
            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/User/Components/ChangePasswordUser.cshtml", selectedId));
        }
    }
}
