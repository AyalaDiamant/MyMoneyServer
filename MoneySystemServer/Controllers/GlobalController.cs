using Logic;
using Logic.DTO;
using Logic.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MoneySystemServer.Code;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Security.Claims;


namespace MoneySystemServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [CheckToken(Order = 50)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GlobalController : ControllerBase
    {        
        protected GResult<bool> Success()
        {
            return Success(true);
        }

        protected Result Fail(string message = null)
        {
            return new Result()
            {
                Success = false,
                Message = message
            };
        }
        protected GResult<T> Fail<T>(T value, string message = null)
        {
            return new GResult<T>()
            {
                Success = false,
                Value = value

            };
        }

        protected GResult<T> Success<T>(T value)
        {
            return new GResult<T>()
            {
                Success = true,
                Value = value
            };
        }

        internal static string GetResultDate(string message, object value = null)
        {
            return JsonConvert.SerializeObject(new GResult<object>()
            {
                Value = value,
                Success = false,
                Message = message,
            }, GetSerializerSettings());
        }


        public static JsonSerializerSettings GetSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            };
        }

        internal static ContentResult GetJsonResult(string message, object value = null)
        {
            return new ContentResult()
            {
                Content = GetResultDate(message, value),
                ContentType = "application/json"
            };
        }

        protected int? UserId
        {
            get
            {
                if (HttpContext.User.Identity is ClaimsIdentity identity)
                {
                    var item = identity.FindFirst(ClaimTypes.Name);
                    if (item != null)
                    {
                        var value = item.Value;
                        if (int.TryParse(value, out int id))
                        {
                            return id;
                        }
                    }

                }


                return null;
            }
        }
        protected int? ManagerId
        {
            get
            {
                if (HttpContext.User.Identity is ClaimsIdentity identity)
                {
                    var item = identity.FindFirst("managerId");
                    if (item != null)
                    {
                        var value = item.Value;
                        if (int.TryParse(value, out int userId))
                        {

                            //    int? id = 0;
                            //    var user = this.dbService.entities.Users.FirstOrDefault(x => x.Id == userId);
                            //    int userTyper = user.UserTypeId;
                            //    if (userTyper == 4)
                            //    {
                            //        id = userId;
                            //    }
                            //    if (userTyper == 2)
                            //    {
                            //        id = user.ManagerId;
                            //    }
                            //    if (userTyper == 5)
                            //    {
                            //        id = user.Lender.ManagerId;
                            //    }
                            //    else
                            //    {

                            //        this.dbService.entities.Users.FirstOrDefault(x => x.UserTypeId == 1).Id = userId;

                            //    };


                            return userId;
                        }
                    }

                }

                return null;
            }
        }

        protected int? SuperUserId
        {
            get
            {
                if (HttpContext.User.Identity is ClaimsIdentity identity)
                {
                    var item = identity.FindFirst("superUserId");
                    if (item != null)
                    {
                        var value = item.Value;
                        if (int.TryParse(value, out int id))
                        {
                            return id;
                        }
                    }
                }

                return null;
            }
        }

        protected int? CustomerId
        {
            get
            {
                if (HttpContext.User.Identity is ClaimsIdentity identity)
                {
                    var item = identity.FindFirst("customerId");
                    if (item != null)
                    {
                        var value = item.Value;
                        if (int.TryParse(value, out int id))
                        {
                            return id;
                        }
                    }

                }

                return null;
            }
        }


    }
}
