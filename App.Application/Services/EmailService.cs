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

    // Triển khai phương thức SendPasswordResetEmail
    public async Task SendPasswordResetEmail(string email, string resetToken)
    {
        // Mô phỏng việc gửi email bằng cách hiển thị thông tin lên console
        // Thực tế bạn nên sử dụng một dịch vụ gửi email như SMTP, SendGrid, hoặc một dịch vụ tương tự

        string subject = "Password Reset Request";
        string body = $"You have requested to reset your password. Use the following token to reset your password: {resetToken}";

        // Ví dụ giả lập gửi email
        Console.WriteLine($"Sending email to {email}");
        Console.WriteLine($"Subject: {subject}");
        Console.WriteLine($"Body: {body}");

        // Chờ giả lập thời gian gửi email
        await Task.Delay(1000);

        Console.WriteLine("Email sent successfully!");
    }
}