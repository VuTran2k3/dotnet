using NeoGym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NeoGym
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "ProductDetail",
                url: "{type1}/{type2}/{meta}",
                new { controller = "Product", action = "Detail" , meta = UrlParameter.Optional },
                new RouteValueDictionary { {"type1", "san-pham" }, 
                                            {"type2", "chi-tiet" } },
                namespaces: new[] { "NeoGym.Controllers" }
            );
            routes.MapRoute(
                name: "Product", 
                url: "{type}/{meta}",
                new { controller = "Product", action = "Index" , meta = UrlParameter.Optional },
                new RouteValueDictionary { { "type", "san-pham"} },
                namespaces: new[] {"NeoGym.Controllers"}
            );
            routes.MapRoute(
                name: "News",
                url: "{type}",
                new { controller = "News", action = "Index" },
                new RouteValueDictionary { { "type", "tin-tuc-su-kien" } },
                namespaces: new[] { "NeoGym.Controllers" }
            );
            routes.MapRoute(
                name: "Club",
                url: "{type}",
                new { controller = "Club", action = "Index" },
                new RouteValueDictionary { { "type", "cau-lac-bo" } },
                namespaces: new[] { "NeoGym.Controllers" }
            );

            routes.MapRoute(
                name: "Contact",
                url: "{type}",
                new { controller = "Contact", action = "Index" },
                new RouteValueDictionary { { "type", "lien-he" } },
                namespaces: new[] { "NeoGym.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "NeoGym.Controllers" }
            );
        }
    }
}
