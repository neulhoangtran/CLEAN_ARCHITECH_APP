using System.Security.Claims;
using WebApp.Components;
using Microsoft.Extensions.Logging;

namespace WebApp.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AuthenticationMiddleware> _logger; // Thêm logger

        public AuthenticationMiddleware(RequestDelegate next, ILogger<AuthenticationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Kiểm tra nếu cookie chứa token
            var token = context.Request.Cookies["accessToken"];
            _logger.LogInformation("test -- 22222");
            if (!string.IsNullOrEmpty(token))
            {
                // Nếu token tồn tại, thêm vào ClaimsPrincipal để xác thực người dùng
                var claims = JwtParser.ParseClaimsFromJwt(token); // Giải mã token và tạo danh sách claims
                var identity = new ClaimsIdentity(claims, "jwtAuthType");
                context.User = new ClaimsPrincipal(identity); // Cập nhật thông tin người dùng
            }

            // Tiếp tục pipeline request
            await _next(context);
        }
    }

}
