namespace _2024AMS.Pages.Services;

public interface IEmailService
{

    Task SendEmail(string strToName, string strToAddress, string strSubject, string strBody);

}
