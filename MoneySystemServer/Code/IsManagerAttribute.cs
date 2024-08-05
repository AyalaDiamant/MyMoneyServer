using Logic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MoneySystemServer.Controllers;
using Newtonsoft.Json;
using System.Reflection.Emit;
using System.Security.Claims;

namespace MoneySystemServer.Code
{
    public class IsManagerAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)

        {
            if (filterContext.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any())
                return;

            IDBService service = filterContext
               .HttpContext
               .RequestServices
               .GetRequiredService<IDBService>();

            var userId = "";

            if (filterContext.HttpContext.User.Identity is ClaimsIdentity identity2)
            {
                userId = identity2.FindFirst(ClaimTypes.Name)?.Value;
            }

            if (userId != null && userId != "" && int.TryParse(userId, out int thesId1) && thesId1 > 0)
            {
                var user = service.entities.Users.FirstOrDefault(x => x.Id == thesId1);
                if (user != null && user.UserTypeId == 1)
                {
                    return;
                }
                else { 
                    NoPermission(filterContext); 
                }

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
