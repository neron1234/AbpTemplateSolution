using ProjectWebApi.Core.Services.Email.MessageCreators.Common;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWebApi.Core.Services.Email.MessageCreators
{
    public class EmailConfirmationCreator : BaseMessageCreator
    {
        protected override MessageTypeEnum GetMessageType()
        {
            return MessageTypeEnum.EmailConfirmation;
        }

        public override MailMessage CreateMessage()
        {
            return new MailMessage
            {
                Subject = Subject,
                Body = Body,
                IsBodyHtml = true
            };
        }

        #region Constant

        private const string Subject = "Subject";
        private const string Body = @"<div style=""font-family:Roboto,sans-serif;width:100%;""></div>";

        #endregion
    }
}
