using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Helpdesk.Infrastructure.Security;

namespace Helpdesk.Web.Filters
{
    public class AuthenticateAttribute : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (AuthContext.User == null)
            {
                filterContext.Result = new RedirectToRouteResult("Default", new RouteValueDictionary { { "controller", "Home" }, { "action", "Login" } });
            }
        }
    }
}