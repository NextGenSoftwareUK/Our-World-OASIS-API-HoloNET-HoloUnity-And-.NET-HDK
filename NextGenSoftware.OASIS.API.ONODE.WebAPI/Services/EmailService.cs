using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using NextGenSoftware.OASIS.API.DNA.Manager;

namespace NextGenSoftware.OASIS.API.ONODE.WebAPI.Services
{
    public interface IEmailService
    {
        void Send(string to, string subject, string html, string from = null);
    }

    public class EmailService : IEmailService
    {
        // private readonly OASISSettings _OASISSettings;

        //public EmailService(IOptions<OASISDNA> OASISDNA)
        //{
        //   // _OASISSettings = OASISSettings.Value;
        //}

        public EmailService()
        {

        }

        public void Send(string to, string subject, string html, string from = null)
        {
            // create message
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(from ?? OASISDNAManager.OASISDNA.OASIS.Email.EmailFrom);
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var smtp = new SmtpClient();
            //smtp.Connect(_appSettings.SmtpHost, _appSettings.SmtpPort, SecureSocketOptions.StartTls);
            smtp.Connect(OASISDNAManager.OASISDNA.OASIS.Email.SmtpHost, OASISDNAManager.OASISDNA.OASIS.Email.SmtpPort, SecureSocketOptions.None);
            smtp.Authenticate(OASISDNAManager.OASISDNA.OASIS.Email.SmtpUser, OASISDNAManager.OASISDNA.OASIS.Email.SmtpPass);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}