namespace WebClient.Helpers;

public interface IEmailHelper
{
    Task SendEmailAsync(string email, string subject, string body);

}
