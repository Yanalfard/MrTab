using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrTab.ViewComponents.Admin
{
    public class CommentVideoReportListInAdminVm : ViewComponent
    {
        private Core db = new Core();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/CommentVideoReport/Components/CommentVideoReportListInAdmin.cshtml", db.Comment.Get(i => i.DocId != null).Where(i => i.IsReported == true).OrderByDescending(i => i.DateSubmited)));
        }
    }
}
