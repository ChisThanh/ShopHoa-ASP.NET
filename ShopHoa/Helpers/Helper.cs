using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Mvc;
using ShopHoa.Identitty;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Web.Helpers;
using FormCollection = System.Web.Mvc.FormCollection;
using Microsoft.AspNet.Identity.Owin;

namespace ShopHoa.Helpers
{
    public class Helper
    {
        public static Task<bool> SendEmail(string toAddress, string subject, string body)
        {
            string fromAddress = "chithanh18042003@gmail.com";
            string password = "lkhfwvzglbmbvrgs";

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromAddress, password),
                EnableSsl = true,
            };

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(fromAddress),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(toAddress);

            try
            {
                smtpClient.Send(mailMessage);
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return Task.FromResult(false);
            }
        }

    }

}

