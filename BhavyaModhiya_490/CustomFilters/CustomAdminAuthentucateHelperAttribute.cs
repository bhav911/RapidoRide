using BhavyaModhiya_490.SessionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BhavyaModhiya_490.CustomFilters
{
    public class CustomAdminAuthentucateHelperAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return HttpContext.Current.Session["UserRole"].Equals("Admin");
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                new System.Web.Routing.RouteValueDictionary {
                    {"controller",UserSession.UserRole},
                    {"action","Unauthorize"}
                }
            );
        }
    }
}
