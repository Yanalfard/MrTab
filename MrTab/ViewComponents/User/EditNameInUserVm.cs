using DataLayer.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrTab.ViewComponents.User
{
    public class EditNameInUserVm : ViewComponent
    {
        private Core db = new Core();
        public async Task<IViewComponentResult> InvokeAsync(string name)
        {
            EditNameUserVm selectName = new EditNameUserVm();
            selectName.Name = name;
            return await Task.FromResult((IViewComponentResult)View("/Areas/User/Views/Account/Components/EditNameInUser.cshtml", selectName));
        }
    }
}
