using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MrTab.ViewComponents.View
{
    public class BodyColorVm : ViewComponent
    {
        private Core db = new Core();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("/Views/Shared/Components/BodyColorVm/BodyColor.cshtml", db.Config.Get().FirstOrDefault(i => i.Keyword.Contains("MainColor")).Value.ToString()));
        }
    }
}
