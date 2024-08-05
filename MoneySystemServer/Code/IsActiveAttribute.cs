using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MoneySystemServer.Controllers;
using Org.BouncyCastle.Bcpg;
using System.Security.Claims;

namespace MoneySystemServer.Code
{
    public class IsActiveAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any())
                return;


            var userId = "";

            if (filterContext.HttpContext.User.Identity is ClaimsIdentity identity2)
            {
                userId = identity2.FindFirst(ClaimTypes.Name)?.Value;
            }

            if (userId != null && userId != "" && int.TryParse(userId, out int thesId1) && thesId1 > 0)
                return;

            var isActive = "";

            if (filterContext.HttpContext.User.Identity is ClaimsIdentity identity3)
            {
                isActive = identity3.FindFirst("isActive")?.Value;
            }
            if (isActive != "true")
            {
                NoPermission(filterContext);
            }
            base.OnActionExecuting(filterContext);
        }
     

        private static void NoPermission(ActionExecutingContext filterContext)
        {
            filterContext.Result = new ContentResult()
            {
                Content = GlobalController.GetResultDate("מצטערים, אין הרשאות לביצוע פעולה זו", 1412),
                ContentType = "application/json",
            };
        }
    }
}
