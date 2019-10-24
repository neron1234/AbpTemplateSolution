using ProjectWebApi.Core.Services.Email.MessageCreators.Common;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWebApi.Core.Services.Email.MessageCreators
{
    public class NotifyAfterEmailConfirmationCreator : BaseMessageCreator
    {
        private string _organizationName;
        private string _firstName;
        private string _middleName;

        protected override MessageTypeEnum GetMessageType()
        {
            return MessageTypeEnum.NotifyAfterEmailConfirmation;
        }

        public override MailMessage CreateMessage()
        {
            return new MailMessage
            {
                Subject = Subject,
                Body = Body.Format(_organizationName, _firstName, _middleName),
                IsBodyHtml = true
            };
        }

        public void SetData(string organizationName, string firstName, string middleName)
        {
            _organizationName = organizationName;
            _firstName = firstName;
            _middleName = middleName;
        }


        #region Constant

        private const string Subject = "Subject";
        private const string Body = @"<div style=""font-family:Roboto,sans-serif;width:100%;"">{0} {1} {2}</div>";

        #endregion
    }
}
