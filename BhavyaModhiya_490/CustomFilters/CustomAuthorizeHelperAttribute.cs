﻿using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BhavyaModhiya_490.CustomFilters
{
    public class CustomAuthorizeHelperAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return HttpContext.Current.Session["UserID"] != null && HttpContext.Current.Session["Username"] != null;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    {"controller" , "Login" },
                    {"action" , "SignIn" }
                });
        }
    }
}
