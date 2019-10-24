using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWebApi.Core.Services.Email.MessageCreators.Common
{
    /// <summary>
    /// Massage creators
    /// </summary>
    public abstract class BaseMessageCreator
    {
        public MessageTypeEnum MessageType => GetMessageType();

        public Task<MailMessage> CreateMessageAsync()
        {
            return Task.Run(CreateMessage);
        }

        public abstract MailMessage CreateMessage();
        protected abstract MessageTypeEnum GetMessageType();
    }
}
