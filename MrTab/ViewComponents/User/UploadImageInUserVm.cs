using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrTab.ViewComponents.User
{
    public class UploadImageInUserVm : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("/Areas/User/Views/Account/Components/UploadImageInUser.cshtml"));

        }
    }
}
