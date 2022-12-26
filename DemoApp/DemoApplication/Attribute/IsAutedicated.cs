using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DemoApplication.Attribute
{
    public class IsAutedicated : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {


            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                RedirectToDashboard(filterContext);
            }
        }
        private void RedirectToDashboard(ActionExecutingContext filterContext)
        {
            var redirectTarget = new RouteValueDictionary
            {
              {"action", "Dashboard"},
              {"controller", "Account"}
            };

            filterContext.Result = new RedirectToRouteResult(redirectTarget);
        }

    }

}
