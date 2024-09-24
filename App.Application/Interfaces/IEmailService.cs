namespace App.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendWelcomeEmail(string email, string username);
    }
}
