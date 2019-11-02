using BatiFren.Common.MyExtensionClasses;
using System.Web.Mvc;
using System.Web.Routing;

namespace BatiFren.WebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

           
            routes.MapRoute(
                  name: "PageRoute",
                  url: "{*url}",
                  defaults: new { controller = "Home", action = "Index" },
                  constraints: new { url = new CmsUrlConstraint() },
                  namespaces: new[] { "IstPlay.WebApp.Controllers" }
                  );

            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );
        }
    }
}
