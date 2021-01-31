using DataLayer.Metadata;
using DataLayer.Models;
using DataLayer.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace MrTab.ViewComponents.View
{
    public class ReportCommentVideoVm : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int DocId, int CommentId)
        {
            ReportCommentVideo report = new ReportCommentVideo();
            report.DocId = DocId;
            report.CommentId = CommentId;
            return await Task.FromResult((IViewComponentResult)View("/Views/Blog/Components/ReportCommentVideoVm/ReportCommentVideo.cshtml", report));
        }
    }
}
