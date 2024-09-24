using System.Threading.Tasks;
using App.Domain.Events;

namespace App.Application.Handlers
{
    public class UserRegisteredEventHandler : IEventHandler<UserRegisteredEvent>
    {
        private readonly IEmailService _emailService;

        // Constructor nhận vào IEmailService để gửi email
        public UserRegisteredEventHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        // Phương thức Handle thực thi logic xử lý sự kiện
        public async Task Handle(UserRegisteredEvent @event)
        {
            // Gửi email chào mừng đến người dùng mới đăng ký
            await _emailService.SendWelcomeEmail(@event.Email, @event.Username);
        }
    }
}
