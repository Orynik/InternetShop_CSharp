using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: null,
                url: "",
                defaults: new { controller = "Home", action = "List", page = 1 , genre = (string)null }
            );

            routes.MapRoute(
                null,
                "Page{page}",
                new { controller = "Home", action = "List", genre = (string)null},
                new { page = @"\d+" }
            );

            routes.MapRoute(
                name: null,
                url: "{genre}",
                defaults: new { controller = "Home", action = "List", page = 1}
            );

            routes.MapRoute(
                name: null,
                url: "{genre}/Page{page}",
                defaults: new { controller = "Home", action = "List"},
                constraints: new { page = @"\d+" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Book", action = "List", id = UrlParameter.Optional }
            );
        }
    }
}
