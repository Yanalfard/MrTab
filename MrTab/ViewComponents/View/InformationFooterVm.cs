using DataLayer.Models;
using DataLayer.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrTab.ViewComponents.View
{
    public class InformationFooterVm : ViewComponent
    {
        private Core db = new Core();
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            InformationFooter info = new InformationFooter();
            info.Restaurant = db.Restaurant.Get().Count();
            info.User = db.User.Get().Count();
            info.Comment = db.Comment.Get().Count();
            info.RateCount = db.Restaurant.Get().Sum(i=>i.RateCount);
            return await Task.FromResult((IViewComponentResult)View("/Views/Shared/Components/InformationFooter.cshtml", info));
        }
        
        
    }
}
