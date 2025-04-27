using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using PresentationLayer.Settings;
using PresentationLayer.Utilities;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace PresentationLayer.Helper
{
    public class SmsService(IOptions<SmsSettings> _options) : ISmsService
    {
        public MessageResource SendSms(SmsMessage smsMessage)
        {
            //open connection
            TwilioClient.Init(_options.Value.AccountSID, _options.Value.AuthToken);

            //create message
            var Message = MessageResource.Create(
                body: smsMessage.Body, 
                from: new Twilio.Types.PhoneNumber(_options.Value.TwilioPhoneNumber),
                to:smsMessage.PhoneNumber);

            return Message; 
             
        }
    }
}
