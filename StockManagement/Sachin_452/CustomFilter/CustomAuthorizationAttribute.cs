using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sachin_452.CustomFilter
{

    public class CustomAuthorizationAttribute : AuthorizeAttribute

    {

        protected override bool AuthorizeCore(HttpContextBase httpContext)

        {

            if (SessionHelper.SessionHelper.UserID != 0 && SessionHelper.SessionHelper.EmailID != "" && SessionHelper.SessionHelper.Role != "")
            {
                return true;

            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {

            filterContext.Result = new RedirectToRouteResult(

            new RouteValueDictionary
            {
                { "controller", "Login" },

                { "action", "Login" }
            });

        }

    }
}