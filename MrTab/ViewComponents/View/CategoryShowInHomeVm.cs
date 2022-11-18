using DataLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrTab.ViewComponents.View
{
    public class CategoryShowInHomeVm : ViewComponent
    {
        private Core db = new Core();
        private IRepository _repository;
        public CategoryShowInHomeVm(IRepository repository)
        {
            _repository = repository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categoryList = _repository.GetAllCategoriesWithRestprants();
            var categores = categoryList.OrderByDescending(i => i.TblRestaurant.Count()).Take(_repository.GetAllCategoryCount());
            return await Task.FromResult((IViewComponentResult)View("/Views/Shared/Components/CategoryShowInHomeVm/CategoryShowInHome.cshtml",
                categores));


            //int countCategory = db.Catagory.Get().Count() - 1;
            //return await Task.FromResult((IViewComponentResult)View("/Views/Shared/Components/CategoryShowInHomeVm/CategoryShowInHome.cshtml",
            //   db.Catagory.Get().Where(i => i.IsHome && i.TblRestaurant.Count != 0).OrderByDescending(i => i.TblRestaurant.Count()).Take(countCategory)));
        
        }
    }
}
