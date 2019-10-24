using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectWebApi.Core.Services.Email.MessageCreators.Common
{
    /// <summary>
    /// Message type
    /// </summary>
    public enum MessageTypeEnum
    {
        /// <summary> </summary>
        Undefined = 0,
        /// <summary> Confirm email </summary>
        EmailConfirmation = 1,
        /// <summary> After confirmation, user is good </summary>
        NotifyAfterEmailConfirmation = 2
    }
}
