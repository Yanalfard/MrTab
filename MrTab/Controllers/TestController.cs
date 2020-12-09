using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MrTab.Utilities;

namespace MrTab.Views.Home
{
    public class TestController : Controller
    {
        private readonly IActionDescriptorCollectionProvider _provider;

        public TestController(IActionDescriptorCollectionProvider provider)
        {
            _provider = provider;
        }

        public List<string> GetRoutes()
        {
            List<string> routes = _provider.ActionDescriptors.Items.Select(x => (
              $"{ x.RouteValues["Controller"]}/{x.RouteValues["Action"]}"
            )).ToList();
            return routes;
        }

        public string Index()
        {
            string ans = "";
            GetRoutes().ForEach(i => { ans += $"\'/{i}\',"; });
            return ($"[{ans.Remove(ans.LastIndexOf(','))}]");
        }
    }
}
