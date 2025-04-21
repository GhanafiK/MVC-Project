using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using PresentationLayer.Settings;
using PresentationLayer.Utilities;

namespace PresentationLayer.Helper
{
    public class MailService(IOptions<MailSettings> _options) : IMailService
    {
        public void Send(Email email)
        {

            var mail = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_options.Value.Email),
                Subject = email.Subject,

            };
            // send to whome?
            mail.To.Add(MailboxAddress.Parse(email.TO));
            // the sender and the dispaly name of mail name 
            mail.From.Add(new MailboxAddress(_options.Value.Email, _options.Value.DisplayName));

            // "BodyBuilder" build the body of any message
            var builder = new BodyBuilder();
            builder.TextBody = email.Body;

            //to build body of the message
            mail.Body = builder.ToMessageBody();


            using var smtp = new SmtpClient();

            //open connection with gmail server and applying security certificate
            smtp.Connect(_options.Value.Host, _options.Value.Port, MailKit.Security.SecureSocketOptions.StartTls);

            //email + password (from app passwords) to act as authentucated in the server
            smtp.Authenticate(_options.Value.Email, _options.Value.Password);

            smtp.Send(mail);

            smtp.Disconnect(true);
        }
    }

}
