using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace _2024AMS.Pages.Services;

public class EmailServiceMailKit : IEmailService
{

    private readonly IConfiguration IConfiguration;
    public EmailServiceMailKit(IConfiguration IC)
    {
        IConfiguration = IC;
    }

    public async Task SendEmail(string strToName, string strToAddress, string strSubject, string strBody)
    {
        // Get the email settings from the appsettings.json file.
        bool booAuthenticate = IConfiguration.GetValue<bool>("Email:Authenticate");
        string strUsername = IConfiguration.GetValue<string>("Email:Username");
        string strPassword = IConfiguration.GetValue<string>("Email:Password");
        string strHost = IConfiguration.GetValue<string>("Email:Host");
        int intPort = IConfiguration.GetValue<int>("Email:Port");

        // Build the email message.
        MimeMessage objMimeMessage = new MimeMessage();
        objMimeMessage.From.Add(new MailboxAddress("No Reply", strUsername));
        objMimeMessage.To.Add(new MailboxAddress(strToName, strToAddress));
        objMimeMessage.Subject = strSubject;
        objMimeMessage.Body = new TextPart(TextFormat.Html) { Text = strBody };

        //Send the email message.
        SmtpClient objSmtpClient = new SmtpClient();
        if (booAuthenticate)
        {
            await objSmtpClient.ConnectAsync(strHost, intPort, SecureSocketOptions.StartTls);
            objSmtpClient.Authenticate(strUsername, strPassword);
        }
        else
        {
            await objSmtpClient.ConnectAsync(strHost, intPort, false);
        }
        await objSmtpClient.SendAsync(objMimeMessage);
        await objSmtpClient.DisconnectAsync(true);

    }

}
