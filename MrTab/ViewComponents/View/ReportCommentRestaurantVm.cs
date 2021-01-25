using DataLayer.Metadata;
using DataLayer.Models;
using DataLayer.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MrTab.ViewComponents.View
{
    public class ReportCommentRestaurantVm : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int RestaurantId, int CommentId)
        {
            ReportCommentRestaurant report = new ReportCommentRestaurant();
            report.RestaurantId = RestaurantId;
            report.CommentId = CommentId;
            return await Task.FromResult((IViewComponentResult)View("/Views/Restaurant/Components/ReportCommentRestaurantVm/ReportCommentRestaurant.cshtml", report));
        }
    }
}
