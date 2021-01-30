using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrTab.ViewComponents.Admin
{
    public class CommentVideoListInAdminVm : ViewComponent
    {
        private Core db = new Core();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/CommentVideo/Components/CommentVideoListInAdmin.cshtml", db.Comment.Get(i => i.DocId != null).OrderByDescending(i => i.DateSubmited)));
        }
    }
}
