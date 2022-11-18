using DataLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrTab.ViewComponents.View
{
    public class CategoryVm : ViewComponent
    {
        private Core db = new Core();
        private IRepository _repository;
        public CategoryVm(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //return await Task.FromResult((IViewComponentResult)View("/Views/Home/Components/CategoryVm/Category.cshtml",
            //    db.Catagory.Get().OrderByDescending(i => i.CatagoryId)));
            return await Task.FromResult((IViewComponentResult)View("/Views/Home/Components/CategoryVm/Category.cshtml",
                _repository.GetAllCategotyIcon()));
        }
    }
}
