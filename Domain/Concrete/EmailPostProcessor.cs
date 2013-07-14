using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Abstract;
using Domain.Entities;
using System.Net;
using System.Net.Mail;

namespace Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "orders@example.com";
        public string MailFromAddress = "taskmanager@example.com";

        public bool UseSsl = true;
        public string Username = "MySmptUsername";
        public string Password = "mysmtpPassword";
        public string ServerName = "smtp.example.com";
        public int ServerPort = 587;
        public bool WriteAsFile = false;//для локального сохранения в true
        public string FileLocation = @"c:\";
    }
    public class EmailPostProcessor:IPostProcessor
    {
        private EmailSettings emailSettings;
        public EmailPostProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }
        public void ProcessPost(ShippingDetails shippingInfo)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);
                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                MailMessage mailMessage = new MailMessage(

                    emailSettings.MailFromAddress,
                    shippingInfo.EmailToAddress,
                    shippingInfo.Title,
                    shippingInfo.Message
                    );

                smtpClient.Send(mailMessage);
            }
        }
    }
}
