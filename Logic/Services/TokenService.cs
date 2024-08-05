using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{

    public interface ITokenService
    {
        void AddUserToDictionary(int id, string token, DateTime createdDate);
        void RemoveUserId(int id);
        List<ActiveUser> GetActiveUsers(int customerId);
        CheckTokenResult CheckTheToken(int id, string token, DateTime createdDate);
        LogoutUserResult LogoutUser(int userId);
    }

    public class TokenService : ITokenService
    {
        #region CTOR

        private ConcurrentDictionary<int, ActiveUser> DictionatyToken = new();
        private readonly IServiceProvider serviceProvider;

        public TokenService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        #endregion


        public void AddUserToDictionary(int id, string token, DateTime loginDate)
        {
            var value = new ActiveUser
            {
                Token = token,
                LoginDate = loginDate,
                LastVisitDate = DateTime.Now,
            };

            _ = DictionatyToken.AddOrUpdate(id, value, (id, stored) =>
            {
                stored.Token = value.Token;
                stored.LastVisitDate = value.LastVisitDate;
                stored.LoginDate = value.LoginDate;

                return value;
            });
        }

        public void RemoveUserId(int id)
        {
            DictionatyToken.TryRemove(id, out _);
        }

        public CheckTokenResult CheckTheToken(int id, string token, DateTime createdDate)
        {
            token = token[7..];

            var value = new ActiveUser
            {
                Token = token,
                LoginDate = createdDate,
                LastVisitDate = DateTime.Now,
            };

            value = DictionatyToken.GetOrAdd(id, value);

            if (!value.IsActive)
                return CheckTokenResult.ManagerLoggedOut;

            value.LastVisitDate = DateTime.Now;

            if (value.Token != token)
                return CheckTokenResult.OtherUser;

            return CheckTokenResult.Success;
        }

        public List<ActiveUser> GetActiveUsers(int customerId)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<MyMoneyBContext>();

            var activeUserList = new List<ActiveUser>();
            var userList = context.Users.Select(c => new { c.Id, c.Email }).ToList();

            foreach (var dict in DictionatyToken)
            {
                if (dict.Value.IsActive)
                {
                    var user = userList.FirstOrDefault(x => x.Id == dict.Key);
                    if (user != null)
                    {
                        dict.Value.UserName = user.Email;
                        dict.Value.UserId = user.Id;
                        activeUserList.Add(dict.Value);
                    }
                }
            }
            return activeUserList;
        }

        public LogoutUserResult LogoutUser(int userId)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<MyMoneyBContext>();
            var customer = context.Users.Any(x => x.Id == userId);

            if (customer == false)
                return LogoutUserResult.CustomerNotExsist;

            if (!DictionatyToken.ContainsKey(userId))
                return LogoutUserResult.UserNotInDictionary;

            DictionatyToken[userId].IsActive = false;

            return LogoutUserResult.Success;
        }

        public void LogoutAllUsersFromList(List<int> userList)
        {
            foreach (var dict in DictionatyToken)
            {
                if (userList.Any(x => x == dict.Key))
                {
                    dict.Value.IsActive = false;
                }
            }
        }
    }

    public class ActiveUser
    {
        public string Token { get; set; }
        public DateTime LoginDate { get; set; }
        public DateTime LastVisitDate { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsSuperUser { get; set; }
    }

    public enum LogoutUserResult
    {
        Success,
        CustomerNotExsist,
        UserNotInDictionary,
        UserNotForCustomer,
        NotFound,
    }

    public enum CheckTokenResult
    {
        ManagerLoggedOut,
        OtherUser,
        Success
    }
}
