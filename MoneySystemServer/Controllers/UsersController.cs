using Logic;
using Logic.DTO;
using Logic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneySystemServer.Code;

namespace MoneySystemServer.Controllers
{
    public class UsersController : GlobalController
    {
        private IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }
        [IsActive]
        [IsPermission]
        [HttpPost]
        public GResult<List<UserDTO>> GetUsers(UserSerach userSerach)
        {
        
            return Success(userService.GetUsers(UserId.Value, userSerach));
        }

        [HttpGet("{id}")]
        public GResult<UserDTO> GetUser(int id)
        {

            return Success(userService.GetUser(id));
        }



        [AllowAnonymous]
        [HttpPost]
        public Result AddUser(UserDTO newUser)
        {

            //var a = userTypeDTO.systemAdministrator;
            //var task = sessionService.GetCurrentUser();
            //UserDTO currentUser = null;
            //if (task != null)
            //{
            //    currentUser = task.Result;
            //}
            int userId = 0;
            if (UserId.Value > 0)
            {
                userId= UserId.Value;
            }
            var isEmailExist = userService.AddUser(newUser, userId);
            if (isEmailExist)
            {
                return Fail(message: "user with such email already exist");
            }
            else
            {
                //if (newUser.UserType.Id == 1)
                if (newUser.UserType.Id == (int)userTypeDTO.systemAdministrator)
                {
                    ChangeUser2Manager(newUser.Id);
                }
            }


            return Success();



        }


        [HttpPut]
        public Result UpdateUser(UserDTO user)
        {
            var isEmailExist = userService.UpdateUser(user);
            if (isEmailExist)
            {
                return Fail(message: "user with such email already exist");
            }
            else
            {
                //if (user.UserType.Id == 1)
                if (user.UserType.Id == (int)userTypeDTO.systemAdministrator)
                {
                    ChangeUser2Manager(user.Id);
                }
                return Success();
            }
        }
        [IsActive]
        [IsManager]
        private void ChangeUser2Manager(int id)
        {
            userService.ChangeUser2Manager(id);
        }

        [IsActive]
        [IsPermission]
        [HttpDelete("{id}")]
        public Result DeleteUser(int id)
        {
            var isUserExist = userService.DeleteUser(id);
            if (!isUserExist)
            {
                return Fail(message: "user not found");

            }
            return Success();
        }

        [IsPermission]
        [IsActive]
        [HttpGet]
        public GResult<List<IdName>> GetUserTypes()
        {
            //var task = sessionService.GetCurrentUser();
            //UserDTO currentUser = null;
            //if (task != null)
            //{
            //    currentUser = task.Result;
            //}
            return Success(userService.GetUserTypes(UserId.Value));
        }

        [HttpPut]
        public Result ChangeUserTypeOrLenderAndDelete(LenderParams lenderParams)
        {
            var succes = userService.ChangeUserTypeOrLenderAndDelete(lenderParams.oldLender, lenderParams.userType, lenderParams.newLender);
            return Success();
        }

        [HttpGet]
        public GResult<List<userTypeDTO>> getAllUserType()
        {
            return Success(userService.GetAllUserType());

        }

    }
}
