using DataLayer.Metadata;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MrTab.ViewComponents.View
{
    public class ReportRestaurantVm : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            TblReport report = new TblReport();
            report.RestaurantId = id;
            return await Task.FromResult((IViewComponentResult)View("/Views/Restaurant/Components/ReportRestaurantVm/ReportRestaurant.cshtml", report));
        }
    }
}
