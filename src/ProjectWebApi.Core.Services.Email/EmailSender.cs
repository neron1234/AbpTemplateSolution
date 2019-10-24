using Abp.Net.Mail;
using ProjectWebApi.Core.Services.Email.MessageCreators;
using ProjectWebApi.Core.Services.Email.MessageCreators.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectWebApi.Core.Services.Email
{
    /// <summary>
    /// Typed email sender
    /// </summary>
    public class EmailSender
    {
        private readonly ConcurrentDictionary<MessageTypeEnum, BaseMessageCreator> _messageCreators;

        /// <summary>
        /// Client
        /// </summary>
        public IEmailSender SenderClient { get; }

        /// <summary>
        /// Typed email sender
        /// </summary>
        public EmailSender(IEmailSender senderClient)
        {
            _messageCreators = new ConcurrentDictionary<MessageTypeEnum, BaseMessageCreator>();

            SenderClient = senderClient;
        }

        /// <summary>
        /// Registers message creator
        /// </summary>
        public void RegisterMessageCreator(BaseMessageCreator creator)
        {
            var creatorId = creator.MessageType;

            if (creatorId == MessageTypeEnum.Undefined)
            {
                throw new ArgumentOutOfRangeException(nameof(creatorId));
            }
            else if (_messageCreators.ContainsKey(creatorId))
            {
                throw new Exception($"Message creator with id={creatorId} already existed.");
            }

            SpinWait.SpinUntil(() => !_messageCreators.TryAdd(creatorId, creator));
        }

        /// <summary>
        /// Send typed message
        /// </summary>
        public void SendMessage(MessageTypeEnum messageType, params string[] toEmails)
        {
            if(!_messageCreators.TryGetValue(messageType, out var messageCreator))
            {
                throw new Exception($"Message creator with id={messageType} not found.");
            }

            // Get message
            var message = messageCreator.CreateMessage();

            // Set receivers
            foreach (var toEmail in toEmails)
            {
                message.To.Add(toEmail);
            }

            // Send
            SenderClient.Send(message);
        }

        /// <summary>
        /// Send typed message
        /// </summary>
        public async Task SendMessageAsync(MessageTypeEnum messageType, params string[] toEmails)
        {
            if (!_messageCreators.TryGetValue(messageType, out var messageCreator))
            {
                throw new Exception($"Message creator with id={messageType} not found.");
            }

            // Get message
            var message = await messageCreator.CreateMessageAsync();

            // Set receivers
            foreach (var toEmail in toEmails)
            {
                message.To.Add(toEmail);
            }

            // Send
            await SenderClient.SendAsync(message);
        }

        /// <summary>
        /// Return message creator for setting custom parameters
        /// </summary>
        public bool TryGetCreator<T>(MessageTypeEnum messageType, out T creator)
            where T : BaseMessageCreator
        {
            if(_messageCreators.TryGetValue(messageType, out var iCreator))
            {
                creator = (T)iCreator;
                return true;
            }

            creator = default;
            return false;
        }
    }
}
