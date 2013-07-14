using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
//ing System.Web.UI

namespace WebUI.Infrastructure
{
    public class SharweAuthorizeAttribute : AuthorizeAttribute
    {

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (SessionManager.CheckSession(httpContext.Session.SessionID) == true)
                if (SessionManager.CheckUserIsInRole(Roles)) 
                    return true;
                else
                    return false;
            else return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (SessionManager.CheckSession(filterContext.HttpContext.Session.SessionID) == false)
            {
                filterContext.Result = new RedirectToRouteResult(
                                new RouteValueDictionary 
                        {
                            { "Account", "LogOn" }
                        });
            }
            else
                base.HandleUnauthorizedRequest(filterContext);
        }
    }
}