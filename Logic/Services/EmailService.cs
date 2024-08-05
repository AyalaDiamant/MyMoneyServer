using Logic.DTO;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Logic.Services
{
    public class EmailService
    {
        private IOptions<EmailConfiguration> config;
        public EmailService(IOptions<EmailConfiguration> config)
        {
            this.config = config;
        }
    
        public void SendEmail(ContactDTO model)
        {
            MailMessage mm = new MailMessage();

            if (model.From != null)
            {
                mm.From = new MailAddress(model.From);
            }
            else
            {
                mm.From = new MailAddress(config.Value.From);
            }

            if (model.From != null)
            {
                mm.ReplyToList.Add(model.From);
            }
            //else if (model.FillConfigReplayTo)
            //{
            //    mm.ReplyToList.Add(config.Value.ReplayTo);
            //}
            mm.Subject = model.Subject;
            mm.Body = model.Body;
            mm.IsBodyHtml = false;

            //if (mm.IsBodyHtml)
            //{
            //    mm.Body = "<div dir=rtl style='text-align:right'>" + mm.Body + "</div>";
            //}

           // mm.To.Add(new MailAddress(model.))
            if (model.mails != null)
            {
                foreach (var item in model.mails)
                {
                    mm.To.Add(new MailAddress(item));

                  //  mm.To.Add(item);
                }

            }
            else
            {
                mm.To.Add(mm.From);
            }

            //SmtpClient client = new SmtpClient(config.Value.Server, config.Value.Port)
            //{
            //    Credentials = new NetworkCredential(config.Value.Username, config.Value.Password)
            //};
            //client.EnableSsl = true;
            SmtpClient mailClient = new SmtpClient(config.Value.Server);
            mailClient.Credentials = new NetworkCredential(config.Value.Username, config.Value.Password);
            mailClient.Port = config.Value.Port;
            mailClient.EnableSsl = true;
           // mailClient.UseDefaultCredentials = true;
            mailClient.Send(mm);
        }
    }

    public class EmailConfiguration
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string From { get; set; }
        public string VC { get; set; }
        public string ReplayTo { get; set; }
    }

}
