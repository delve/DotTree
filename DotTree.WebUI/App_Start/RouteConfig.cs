using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DotTree.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                null,
                "",
                new { Controller = "Person", action = "List", family = 0, page = 1 }
                );

            routes.MapRoute(
                null,
                "Page{page}",
                new { controller = "Person", action = "List", family = 0 },
                new { page = @"\d+" }
                );

            routes.MapRoute(
                null,
                "{family}",
                new { controller = "Person", action = "List", page = 1 }
                );

            routes.MapRoute(
                null,
                "{family}/Page{page}",
                new { controller = "Person", action = "List" },
                new { page = @"\d+" }
                );

            routes.MapRoute(null, "{controller}/{action}");
        }
    }
}
