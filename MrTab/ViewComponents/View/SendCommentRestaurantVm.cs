using DataLayer.Metadata;
using DataLayer.Models;
using DataLayer.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace MrTab.ViewComponents.View
{
    public class SendCommentRestaurantVm : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            SendCommentVm comment = new SendCommentVm();
            comment.RestaurantId = id;
            return await Task.FromResult((IViewComponentResult)View("/Views/Restaurant/Components/SendCommentRestaurantVm/SendCommentRestaurant.cshtml", comment));
        }
    }
}
