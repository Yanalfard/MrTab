using Microsoft.AspNetCore.Mvc;
using MrTab.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrTab.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("admin")]
    public class CommentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
