using DataLayer.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrTab.ViewComponents.View
{
    public class UploadImageRestaurantVm : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            UploadImageRestaurant uploadImage = new UploadImageRestaurant();
            uploadImage.id = id;
            return await Task.FromResult((IViewComponentResult)View("/Views/Restaurant/Components/UploadImageRestaurantVm/UploadImageRestaurant.cshtml", uploadImage));
        }
    }
}
