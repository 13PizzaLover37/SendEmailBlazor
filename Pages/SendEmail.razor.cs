using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

// Function for sending email 
// I use ethereal.email for get email address for sending email 
// to_person / subject / body we are getting from inputs on our page

namespace SendEmailBlazor.Pages
{
    public partial class SendEmail
    {
        // email person who will get message
        string to_person { get; set; }

        // title
        string? subject { get; set; }

        // body our message
        string? body { get; set; }

        public async void sendMessage()
        {
            MimeMessage emailMessage = new MimeMessage();
            // my email from ethereal.email
            emailMessage.From.Add(MailboxAddress.Parse("otis93@ethereal.email"));
            emailMessage.To.Add(MailboxAddress.Parse(to_person));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

            using (var smtpClient = new SmtpClient())
            {
                try
                {
                    // create server part for send info
                    smtpClient.ServerCertificateValidationCallback = MySslCertificateValidationCallback;
                    smtpClient.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
                    smtpClient.Authenticate("otis93@ethereal.email", "shYchu1PR2fv1wgXSz");
                    smtpClient.Send(emailMessage);
                    smtpClient.Disconnect(true);
                } catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

        }
        // for don't paying attention to errors :)
        static bool MySslCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}

