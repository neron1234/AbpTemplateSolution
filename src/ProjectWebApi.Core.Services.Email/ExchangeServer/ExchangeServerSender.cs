using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Net.Mail;
using Microsoft.Exchange.WebServices.Data;
using System;
using System.Net;
using System.Net.Mail;
using Task = System.Threading.Tasks.Task;

namespace ProjectWebApi.Core.Services.Email.ExchangeServer
{
    public class ExchangeServerSender : IEmailSender
    {
        private readonly ISettingManager _settings;
        private ExchangeService _exchangeService;

        public ExchangeServerSender(ISettingManager settings)
        {
            ServicePointManager.ServerCertificateValidationCallback = (obj, certificate, chain, errors) => true;
            _settings = settings;
        }

        public void Send(string to, string subject, string body, bool isBodyHtml = true)
        {
            Configure();
            EmailMessage message = new EmailMessage(_exchangeService);
            message.ToRecipients.Add(to);
            message.CreateContent(subject, body, isBodyHtml);
            message.Send();
        }

        public void Send(string from, string to, string subject, string body, bool isBodyHtml = true)
        {
            Configure();
            EmailMessage message = new EmailMessage(_exchangeService);
            message.From = from;
            message.ToRecipients.Add(to);
            message.CreateContent(subject, body, isBodyHtml);
            message.Send();
        }

        public void Send(MailMessage mail, bool normalize = true)
        {
            Configure();
            EmailMessage message = new EmailMessage(_exchangeService);
            message.CreateContent(mail, normalize);
            message.Send();
        }

        public Task SendAsync(string to, string subject, string body, bool isBodyHtml)
        {
            Configure();
            EmailMessage message = new EmailMessage(_exchangeService);
            message.ToRecipients.Add(to);
            message.CreateContent(subject, body, isBodyHtml);
            return Task.Run(() => message.Send());
        }

        public Task SendAsync(string from, string to, string subject, string body, bool isBodyHtml)
        {
            Configure();
            EmailMessage message = new EmailMessage(_exchangeService);
            message.From = from;
            message.ToRecipients.Add(to);
            message.CreateContent(subject, body, isBodyHtml);
            return Task.Run(() => message.Send());
        }

        public Task SendAsync(MailMessage mail, bool normalize = true)
        {
            Configure();
            EmailMessage message = new EmailMessage(_exchangeService);
            message.CreateContent(mail, normalize);
            return Task.Run(() => message.Send());
        }

        private void Configure()
        {
            if (_exchangeService == null)
            {
                var exchangeUrl = _settings.GetSettingValue(Host);
                var userName = _settings.GetSettingValue(UserName);
                var password = _settings.GetSettingValue(Password);
                
                _exchangeService = new ExchangeService(ExchangeVersion.Exchange2007_SP1)
                {
                    Url = new Uri(exchangeUrl),
                    Credentials = new WebCredentials(userName, password),
                };
            }
        }

        public static string Host => "ExchangeServer.Host";
        public static string UserName => "ExchangeServer.UserName";
        public static string Password => "ExchangeServer.Password";
    }
}
