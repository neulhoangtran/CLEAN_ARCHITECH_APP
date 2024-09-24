using System.Threading.Tasks;
using App.Application.Interfaces;

public class EmailService : IEmailService
{
    public Task SendWelcomeEmail(string email, string username)
    {
        // Thực thi logic gửi email chào mừng đến người dùng mới
        Console.WriteLine($"Sending welcome email to {email} for user {username}");
        return Task.CompletedTask;
    }
}