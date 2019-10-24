using Microsoft.Exchange.WebServices.Data;
using System.Net.Mail;

namespace ProjectWebApi.Core.Services.Email.ExchangeServer
{
    public static class MessageExtensions
    {
        public static EmailMessage CreateContent(this EmailMessage message, string subject, string body, bool isBodyHtml = true)
        {
            message.Subject = subject;
            message.Body = body;
            message.Body.BodyType = isBodyHtml ? BodyType.HTML : BodyType.Text;
            return message;
        }

        /// <summary>
        /// Casts mailMessage to EmailMessage
        /// </summary>
        public static EmailMessage CreateContent(this EmailMessage message, MailMessage mail, bool normalize = true)
        {
            message.Subject = mail.Subject;
            message.Body = mail.Body;
            message.Body.BodyType = mail.IsBodyHtml ? BodyType.HTML : BodyType.Text;
            
            foreach (var e in mail.To)
            {
                message.ToRecipients.Add(new EmailAddress(e.Address));
            }

            foreach (var a in mail.Attachments)
            {
                string fileName = a.Name;
                var contentStream = a.ContentStream;
                message.Attachments.AddFileAttachment(fileName, contentStream);
            }

            if (normalize)
            {
                // normalize?
            }

            return message;
        }
    }
}
