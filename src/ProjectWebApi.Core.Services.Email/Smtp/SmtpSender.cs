using Abp.MailKit;
using Abp.Net.Mail;
using MimeKit;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ProjectWebApi.Core.Services.Email.Smtp
{
    public class SmtpSender : MailKitEmailSender
    {
        public SmtpSender(IEmailSenderConfiguration smtpEmailSenderConfiguration, IMailKitSmtpBuilder smtpBuilder)
           : base(smtpEmailSenderConfiguration, smtpBuilder)
        {

        }

        protected override void SendEmail(MailMessage mail)
        {
            using (var client = BuildSmtpClient())
            {
                var message = GetMimeMessage(mail);
                client.Send(message);
                client.Disconnect(true);
            }
        }

        protected override async Task SendEmailAsync(MailMessage mail)
        {
            using (var client = BuildSmtpClient())
            {
                var message = GetMimeMessage(mail);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }

        private MimeMessage GetMimeMessage(MailMessage mailMessage)
        {
            var message = MimeMessage.CreateFromMailMessage(mailMessage);

            SetAttachmentFileNameEncodingRfc2047(message.Attachments);

            return message;
        }

        /// <summary>
        /// Fix bad file names encoding in Outlook
        /// </summary>
        /// <param name="attachments"></param>
        private void SetAttachmentFileNameEncodingRfc2047(IEnumerable<MimeEntity> attachments)
        {
            foreach (var attachment in attachments)
            {
                if (attachment.ContentDisposition.Parameters.TryGetValue("filename", out Parameter param))
                {
                    param.EncodingMethod = ParameterEncodingMethod.Rfc2047;
                }
            }
        }
    }
}
