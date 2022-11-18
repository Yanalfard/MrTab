using DataLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrTab.ViewComponents.View
{
    public class CategoryShowInHomeSingleVm : ViewComponent
    {
        private Core db = new Core();
        private IRepository _repository;
        public CategoryShowInHomeSingleVm(IRepository repository)
        {
            _repository = repository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var llll= _repository.GetSingleCategory();
            //return await Task.FromResult((IViewComponentResult)View("/Views/Shared/Components/CategoryShowInHomeSingleVm/CategoryShowInHomeSingle.cshtml",
            //   db.Catagory.Get().Where(i => i.IsHome && i.TblRestaurant.Count != 0).OrderByDescending(i => i.CatagoryId).Take(1)));
            return await Task.FromResult((IViewComponentResult)View("/Views/Shared/Components/CategoryShowInHomeSingleVm/CategoryShowInHomeSingle.cshtml",
                _repository.GetSingleCategory()));
        }
    }

}
