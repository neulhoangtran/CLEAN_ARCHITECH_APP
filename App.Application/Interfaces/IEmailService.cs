namespace App.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendWelcomeEmail(string email, string username);

        // Phương thức gửi email đặt lại mật khẩu
        Task SendPasswordResetEmail(string email, string resetToken);
    }
}
