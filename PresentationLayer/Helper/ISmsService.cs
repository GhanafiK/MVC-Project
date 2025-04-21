using PresentationLayer.Utilities;
using Twilio.Rest.Api.V2010.Account;

namespace PresentationLayer.Helper
{
    public interface ISmsService
    {
        MessageResource SendSms(SmsMessage smsMessage);
    }
}
