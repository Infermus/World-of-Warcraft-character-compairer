using System;
using System.Net;
using System.Net.Mail;
using WowCharComparerWebApp.Configuration;
using WowCharComparerWebApp.Models.DataTransferObject;

namespace WowCharComparerWebApp.Notifications
{
    public class EmailManager
    {
        public EmailSendStatus SendMail(string userEmail, string emailSubject, string emailBody)
        {
            EmailSendStatus emailSendStatus = new EmailSendStatus();

            try
            {
                using (MailMessage msg = new MailMessage())
                using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
                {
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(APIConf.WowCharacterComparerEmail, APIConf.WoWCharacterComparerEmailPassword),
                    Timeout = 100000,
                    EnableSsl = true
                })
                {
                    msg.To.Add(userEmail);
                    msg.From = new MailAddress(APIConf.WowCharacterComparerEmail);
                    msg.IsBodyHtml = true;
                    msg.Subject = emailSubject;
                    msg.Body = emailBody;
                    client.Send(msg);
                }

                emailSendStatus.SendSuccessfully = true;
            }
            catch (Exception ex)
            {
                emailSendStatus.SendEmailException = ex;
                emailSendStatus.SendSuccessfully = false;          
            }

            return emailSendStatus;
        }
    }
}
