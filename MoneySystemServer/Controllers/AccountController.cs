using Logic;
using Logic.DTO;
using Logic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MoneySystemServer.Controllers
{
    [AllowAnonymous]
    public class AccountController : GlobalController
    {
        private IAccountService accountService;
        private EmailService emailService;

        public AccountController(IAccountService accountService, EmailService emailService)
        {
            this.accountService = accountService;
           this.emailService = emailService;
        }

        public async Task<GResult<UserDTO>> SignIn(LoginDTO model)
        {
         //   var token = accountService.SignIn(model);
            var result = accountService.SignIn(model);
            if (result == null)
            {
                return Fail<UserDTO>(null, "Email or password are incorrect");
            }
            else
            {
                if (!result.IsActive)
                {
                    return Fail<UserDTO>(null, "Error 7777");
                }
            }


            //  await sessionService.SetCurrentUser(result);

            return Success(result);
        }

        [HttpPost]
        public Result ForgotPswd(ContactDTO model)
        {
            var pswd = accountService.CheckEmail(model);
            if (pswd != null)
            {
                model.Subject = "שחזור סיסמה";
                model.mails.Add(model.From);
                model.Body = "הסיסמה שלך במערכת היא : " + pswd;
                try
                {
                    emailService.SendEmail(model);
                    return Success();
                }
                catch (Exception ex)
                {
                    return Fail(message: ex.ToString());
                }
            }
            return Fail(message: "no such email");
        }
    }
}
