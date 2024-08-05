using Microsoft.AspNetCore.Mvc.Filters;
using MoneySystemServer.Controllers;

namespace MoneySystemServer.Code
{
    public class IsPermissionAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //var sessionService = context.HttpContext.RequestServices.GetService<ISessionService>();

            //if (sessionService == null)
            //{
            //    context.Result = GlobalController.GetJsonResult("Error 8888");
            //    return;
            //}

            //if (!context.HttpContext.User.Identity.IsAuthenticated)
            //{
            //    context.Result = GlobalController.GetJsonResult("Error 8888");
            //    return;
            //}

            //var user = await sessionService.GetCurrentUser();
            //if (user == null)
            //{
            //    context.Result = GlobalController.GetJsonResult("Error 8888");
            //    return;
            //}
            //bool isPermission = user.UserType.Id == 1 || user.UserType.Id == 5 || user.UserType.Id == 2;
            //if (!isPermission)
            //{
            //    context.Result = GlobalController.GetJsonResult("Error 8888");
            //    return;
            //}

            await next();
        }

    }
}
