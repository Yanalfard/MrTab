using DataLayer.Metadata;
using DataLayer.Models;
using DataLayer.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MrTab.Utilities;
using Services.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MrTab.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("admin")]
    public class AboutContactController : Controller
    {
        private Core db = new Core();
        public IActionResult ContactUs()
        {
            TblConfig updateConfig = db.Config.Get().FirstOrDefault(i => i.Keyword.Contains("ContactUs"));
            ConfigVm selectedConfig = new ConfigVm();
            selectedConfig.Keyword = updateConfig.Keyword;
            selectedConfig.Value = updateConfig.Value;
            return View(selectedConfig);
        }
        [HttpPost]
        public IActionResult ContactUs(ConfigVm config)
        {
            TblConfig updateConfig = db.Config.Get().FirstOrDefault(i => i.Keyword.Contains("ContactUs"));
            if (updateConfig != null)
            {
                updateConfig.Value = config.Value;
                db.Config.Save();
            }
            return View(config);
        }

        public IActionResult Rules()
        {
            TblConfig updateConfig = db.Config.Get().FirstOrDefault(i => i.Keyword.Contains("Gavanin"));
            ConfigVm selectedConfig = new ConfigVm();
            selectedConfig.Keyword = updateConfig.Keyword;
            selectedConfig.Value = updateConfig.Value;
            return View(selectedConfig);
        }
        [HttpPost]
        public IActionResult Rules(ConfigVm config)
        {
            TblConfig updateConfig = db.Config.Get().FirstOrDefault(i => i.Keyword.Contains("Gavanin"));
            if (updateConfig != null)
            {
                updateConfig.Value = config.Value;
                db.Config.Save();
            }
            return View(config);
        }
    }
}
