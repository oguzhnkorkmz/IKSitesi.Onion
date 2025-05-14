using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using IK.InfrastructureLayer.EmailSender;

public class EmailSenderWithMailKit : IEmailSender
{
    private readonly IConfiguration _configuration;

    public EmailSenderWithMailKit(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string htmlMessage)
    {
        var smtpSection = _configuration.GetSection("Smtp");
        var host = smtpSection["Host"];
        var port = int.Parse(smtpSection["Port"]);  // appsettings.json'da port değeri 465 olarak ayarlanabilir, veya kodda doğrudan 465 yazabilirsiniz.
        var username = smtpSection["Username"];
        var password = smtpSection["Password"];
        var fromEmail = smtpSection["FromEmail"];
        var fromName = smtpSection["FromName"];

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(fromName, fromEmail));
        message.To.Add(MailboxAddress.Parse(toEmail));
        message.Subject = subject;

        var bodyBuilder = new BodyBuilder { HtmlBody = htmlMessage };
        message.Body = bodyBuilder.ToMessageBody();

        using (var client = new SmtpClient())
        {
            // Port 465 için SSL/TLS ile doğrudan bağlantı kuruyoruz.
            await client.ConnectAsync(host, 465, SecureSocketOptions.SslOnConnect);
            await client.AuthenticateAsync(username, password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}