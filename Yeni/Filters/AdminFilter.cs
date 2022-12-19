using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Yeni.Filters
{
    public class AdminFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            int? AdminId =context.HttpContext.Session.GetInt32("AdminId");
            if (!AdminId.HasValue)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "action","Index" },
                    {"controller","Admin" }
                    });
            }
            base.OnActionExecuting(context);
        }
    }
}
