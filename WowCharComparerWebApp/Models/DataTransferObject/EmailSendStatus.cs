using System;

namespace WowCharComparerWebApp.Models.DataTransferObject
{
    public class EmailSendStatus
    {
        public bool SendSuccessfully;

        public Exception SendEmailException;
    }
}