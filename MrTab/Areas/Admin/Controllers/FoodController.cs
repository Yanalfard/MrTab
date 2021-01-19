using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrTab.Areas.Admin.Controllers
{
    public class FoodController : Controller
    {
        public async Task<IActionResult> Index(int id)
        {
            return await Task.FromResult(View());
        }
    }
}
