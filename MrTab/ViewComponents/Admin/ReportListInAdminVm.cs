using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrTab.ViewComponents.Admin
{
    public class ReportListInAdminVm : ViewComponent
    {
        private Core db = new Core();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Restauran/Components/ReportListInAdmin.cshtml", db.Report.Get()));
        }
    }
}
