using ProjectWebApi.Core.Services.Email.MessageCreators;
using ProjectWebApi.Core.Services.Email.MessageCreators.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectWebApi.Core.Services.Email
{
    /// <summary>
    /// Get message creator helper
    /// </summary>
    public static class EmailSenderExtensions
    {
        public static bool TryGetEmailConfirmationCreator(this EmailSender sender, out EmailConfirmationCreator creator)
        {
           return sender.TryGetCreator(MessageTypeEnum.EmailConfirmation, out creator);
        }

        public static bool TryGetNotifyAfterEmailConfirmationCreator(this EmailSender sender, out NotifyAfterEmailConfirmationCreator creator)
        {
            return sender.TryGetCreator(MessageTypeEnum.NotifyAfterEmailConfirmation, out creator);
        }
    }
}
