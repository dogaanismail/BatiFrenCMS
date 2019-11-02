using BatiFren.Entities;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace BatiFren.Common.MyExtensionClasses
{
    public class CmsUrlConstraint : IRouteConstraint
    {
        public CmsUrlConstraint()
        {

        }
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values[parameterName] != null)
            {
                var url = values[parameterName].ToString();
                url = url.TrimSlash();
                using (BatiFrenDBEntities db = new BatiFrenDBEntities())
                {
                    var page = db.Pages.Where(x => x.Url == url);
                    if (page != null)
                    {
                        return true;
                    }
                    else
                    {

                    }
                }
            }
            return false;
        }
    }
    }

