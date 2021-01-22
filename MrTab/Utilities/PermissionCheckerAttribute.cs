﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Services.Services;
using DataLayer.Models;

namespace MrTab.Utilities
{
    public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private Core db = new Core();
        private string _permissionId = "";
        public PermissionCheckerAttribute(string permissionId)
        {
            _permissionId = permissionId;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                string userName = context.HttpContext.User.Identity.Name;
                TblUser selectUser = db.User.Get().SingleOrDefault(i => i.TellNo == userName);
                if (selectUser.Role.Name != _permissionId)
                {
                    context.Result = new RedirectResult("/Account/Login");
                }
            }
            else
            {
                context.Result = new RedirectResult("/Account/Login");
            }
        }
    }
}
