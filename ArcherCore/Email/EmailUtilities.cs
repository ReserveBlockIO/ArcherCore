using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ArcherCore.Email
{
    public static class EmailUtilities
    {
        public static async Task<(bool, string)> SendStandardEmail(string toAddressStr, string fromAddressStr, string fromAddressName, string fromAddressPassword,
            string subjectStr, string bodyStr, string host, int port, SmtpDeliveryMethod deliveryMethod, bool enableSsl = true, bool useDefaultCreds = false,
            bool isBodyHtml = false, string replyToEmail = "")
        {
            try
            {
                var fromAddress = new MailAddress(fromAddressStr, fromAddressName);
                var toAddress = new MailAddress(toAddressStr);
                string fromPassword = fromAddressPassword;
                string subject = subjectStr;
                string body = bodyStr;

                var smtp = new SmtpClient
                {
                    Host = host,
                    Port = port,
                    DeliveryMethod = deliveryMethod,
                    EnableSsl = enableSsl,
                    UseDefaultCredentials = useDefaultCreds,
                    Credentials = new NetworkCredential(fromAddressStr, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    IsBodyHtml = isBodyHtml,
                    Subject = subject,
                    Body = body,
                    ReplyTo = replyToEmail != "" ? new MailAddress(replyToEmail) : fromAddress,
                })
                {
                    smtp.SendAsync(message, null);
                    return (true, $"[{DateTime.Now}] - Email has been sent.");
                }
            }
            catch (Exception ex)
            {
                return (false, $"[{DateTime.Now}] - Failed to send. Reason: {ex.ToString}");
            }
        }

        public static async Task<(bool, string)> SendStandardEmail(string toAddressStr, string fromAddressStr, string fromAddressName, string fromAddressPassword,
            string subjectStr, string bodyStr, bool isBodyHtml = false, string replyToEmail = "")
        {
            try
            {
                var fromAddress = new MailAddress(fromAddressStr, fromAddressName);
                var toAddress = new MailAddress(toAddressStr);
                string fromPassword = fromAddressPassword;
                string subject = subjectStr;
                string body = bodyStr;

                var smtp = new SmtpClient
                {
                    Host = EmailVariables.Host,
                    Port = EmailVariables.Port,
                    DeliveryMethod = EmailVariables.SmtpDeliveryMethod,
                    EnableSsl = EmailVariables.EnableSSL,
                    UseDefaultCredentials = EmailVariables.UseDefaultCredentials,
                    Credentials = new NetworkCredential(fromAddressStr, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    IsBodyHtml = isBodyHtml,
                    Subject = subject,
                    Body = body,
                    ReplyTo = replyToEmail != "" ? new MailAddress(replyToEmail) : fromAddress,
                })
                {
                    smtp.SendAsync(message, null);
                    return (true, $"[{DateTime.Now}] - Email has been sent.");
                }
            }
            catch (Exception ex)
            {
                return (false, $"[{DateTime.Now}] - Failed to send. Reason: {ex.ToString}");
            }
        }
    }
}
