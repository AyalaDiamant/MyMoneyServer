using Logic.DTO;
using Logic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneySystemServer.Controllers;

namespace Api.Controllers
{
    [AllowAnonymous]
    public class ContactController : GlobalController
    {
        private EmailService emailService;
        private IConfiguration config;
        private DBService dbService;

        public ContactController(EmailService emailService, IConfiguration config)
        {
            this.emailService = emailService;
            this.config = config;
        }

        [HttpPost]
        public Result SendRequest(ContactDTO model)
        {
            model.Subject = "הודעת מייל מאתר תקציב לי";

            string mailTo = "mymonytest@gmail.com";

            var serverType = config["Server:Type"];


            if (serverType == "test")
            {
                mailTo = "mymonytest@gmail.com";
            }

            else if (serverType == "prod")
            {

            }

            model.mails.Add(mailTo);

            try
            {
                model.Body = GetBody(model);
                emailService.SendEmail(model);
                return Success();
            }
            catch (Exception ex)
            {
                return Fail(message: ex.ToString());
            }

        }
        private string GetBody(ContactDTO model)
        {
            string msg = "";
            msg += "קיבלת הודעה מ ";
            msg += model.Name + "\n";
            msg += "כתובת מייל";
            msg += model.From + "\n";
            msg += "בנושא ";
            msg += model.Subject + "\n";
            msg += "התוכן : " + "\n";
            msg += model.Body;

            return msg;
        }

    }
}
