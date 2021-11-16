﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LXP2CYD
{
    public class Emailer
    {
        public static void Send(string to, string subject, string body, bool isBodyHtml)
        {

            MailAddress toEmail = new MailAddress(to);
            MailAddress from = new MailAddress("no-reply@LPX2.com");
            MailMessage mail = new MailMessage(from, toEmail);

            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = isBodyHtml;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.mailtrap.io";
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("f4e7b6f9b00c43", "5930de6e7bd705");
            try
            {
                smtp.Send(mail);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public static void Send(string to, string subject, string body, string fromEmail, bool isBodyHtml)
        {

            MailAddress toEmail = new MailAddress(to);
            MailAddress from = new MailAddress(fromEmail);

            MailMessage mail = new MailMessage(from, toEmail);

            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = isBodyHtml;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.mailtrap.io";
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("5930de6e7bd705", "5930de6e7bd705");

            try
            {
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task SmptSend(string to, string subject, string body, bool isBodyHtml, Dictionary<string, string> credentials)
        {
            try
            {
                MailAddress toEmail = new MailAddress(to);
                MailAddress from = new MailAddress(credentials.GetValueOrDefault("Email", "no-reply@LPX2.com"), credentials.GetValueOrDefault("DisplayName", "Center management team"));
                MailMessage mail = new MailMessage(from, toEmail)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = isBodyHtml
                };
                //SmtpClient smtp = new SmtpClient
                //{
                //    EnableSsl = false,
                //    UseDefaultCredentials = false,
                //    Credentials = new NetworkCredential(credentials.GetValueOrDefault("Email", "no-reply@saleshack.io"), credentials.GetValueOrDefault("Password", "")),
                //    DeliveryMethod = SmtpDeliveryMethod.Network,
                //    Port = int.Parse(credentials.GetValueOrDefault("Port", "26")),
                //    Host = credentials.GetValueOrDefault(AppConsts.SalesHackHost, "mail.saleshack.io")
                //};

                SmtpClient smtp = new SmtpClient
                {
                    EnableSsl = false,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("e5b937e22ce6f4454fc21c9b8c6e2972", "226b2b54b896ded14473e112de3b28c1"),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Port = 587,
                    Host = "in-v3.mailjet.com"
                };
                await smtp.SendMailAsync(mail);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}