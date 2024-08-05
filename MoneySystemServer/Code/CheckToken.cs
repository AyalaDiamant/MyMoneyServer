using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Logic.Services;
using MoneySystemServer.Controllers;

namespace MoneySystemServer.Code
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class CheckTokenAttribute : ActionFilterAttribute
    {
        public bool IsCheck { get; set; } = true;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any())
                return;

            if (filterContext.HttpContext.Response.Headers["cust-flag"].Count > 0)
                return;

            if (!IsCheck)
            {
                filterContext.HttpContext.Response.Headers.Add("cust-flag", new Microsoft.Extensions.Primitives.StringValues("stam"));
                return;
            }


            ITokenService service = filterContext.HttpContext.RequestServices.GetService<ITokenService>();
            var token = filterContext.HttpContext.Request.Headers["Authorization"];

            if (filterContext.HttpContext.User.Identity is not ClaimsIdentity identity)
                return;

            DateTime createdDate = new();
            var date = identity.FindFirst("loginDate")?.Value;
            if (date != null)
            {
                _ = DateTime.TryParse(date, out createdDate);
            }

            //string id = identity.FindFirst("superUserId")?.Value;
            //if (id == null || id.Length == 0 || id == "0")
            //{
            //    id = identity.FindFirst(ClaimTypes.Name)?.Value;
            //}

            //if (id != null && id != "" && int.TryParse(id, out int key) && key > 0)
            //{
            //    var res = service.CheckTheToken(key, token, createdDate);
            //    if (res == CheckTokenResult.ManagerLoggedOut)
            //    {
            //        Fail(filterContext, "מנהל ביצע יציאה עבורך");
            //    }
            //    else if (res == CheckTokenResult.OtherUser)
            //    {
            //        Fail(filterContext, "משתמש זה נכנס ממקום אחר, מתבצעת יציאה");
            //    }
            //}
        }

        private static void Fail(ActionExecutingContext filterContext, string msg)
        {

            filterContext.Result = new ContentResult()
            {
                Content = GlobalController.GetResultDate(msg, 1714),
                ContentType = "application/json",
            };
        }
    }
}
