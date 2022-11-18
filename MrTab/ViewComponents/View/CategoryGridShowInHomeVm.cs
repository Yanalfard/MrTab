using DataLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrTab.ViewComponents.View
{
    public class CategoryGridShowInHomeVm : ViewComponent
    {
        private Core db = new Core();
        private IRepository _repository;
        public CategoryGridShowInHomeVm(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("/Views/Shared/Components/CategoryGridShowInHomeVm/CategoryGridShowInHome.cshtml",
               _repository.CategoryGridShowInHome()));
            //return await Task.FromResult((IViewComponentResult)View("/Views/Shared/Components/CategoryGridShowInHomeVm/CategoryGridShowInHome.cshtml",
            //    db.Catagory.Get().Where(i => i.IsHome && i.ImageUrl != null).OrderByDescending(i => i.CatagoryId).Take(8)));
        }
    }
}
