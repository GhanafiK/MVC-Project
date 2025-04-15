using System.Net;
using System.Net.Mail;

namespace PresentationLayer.Utilities
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var Client=new SmtpClient("smtp.gmail.com", 587);
            Client.EnableSsl = true;
            Client.Credentials = new NetworkCredential("gamalhanafi26@gmail.com", "lmckygbucehtpvyy");
            Client.Send("gamalhanafi26@gmail.com", email.TO, email.Subject, email.Body);
        }
    }
}
