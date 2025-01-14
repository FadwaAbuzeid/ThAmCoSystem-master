using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

public class EmailService
{
    private readonly string _sendGridApiKey = "your-sendgrid-api-key";

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var client = new SendGridClient(_sendGridApiKey);
        var from = new EmailAddress("support@threeamigos.com", "ThreeAmigos");
        var to = new EmailAddress(toEmail);
        var msg = MailHelper.CreateSingleEmail(from, to, subject, body, body);
        await client.SendEmailAsync(msg);
    }
}
