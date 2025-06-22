using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class EmailService : IEmailService
{
    private readonly string _from = "naszmail@gmail.com";
    private readonly string _smtpHost = "smtp.com";
    private readonly int _smtpPort = 587;
    private readonly string _smtpUser = "admin@gmail.com";
    private readonly string _smtpPass = "admin123";

    public async Task SendReceiptAsync(string email, string subject, string content)
    {
        var message = new MailMessage(_from, email, subject, content);
        message.IsBodyHtml = false;
        using var client = new SmtpClient(_smtpHost, _smtpPort)
        {
            Credentials = new NetworkCredential(_smtpUser, _smtpPass),
            EnableSsl = true
        };

        await client.SendMailAsync(message);
    }
}