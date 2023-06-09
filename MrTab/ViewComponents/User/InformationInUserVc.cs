﻿using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MrTab.ViewComponents.User
{
    public class InformationInUserVc: ViewComponent
    {
        private Core db = new Core();
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            int userId = Convert.ToInt32(id);
            TblUser selectUser = db.User.GetById(userId);
            return await Task.FromResult((IViewComponentResult)View("/Areas/User/Views/Account/Components/InformationInUserVc.cshtml", selectUser));
        }
    }
}
