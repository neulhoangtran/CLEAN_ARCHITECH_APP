using System.Security.Claims;
using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;
using WebApp.Components;

namespace WebApp.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AuthenticationMiddleware> _logger;

        public AuthenticationMiddleware(RequestDelegate next, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, ILogger<AuthenticationMiddleware> logger)
        {
            _next = next;
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Nếu người dùng đã có thông tin xác thực, bỏ qua middleware này
            if (context.User.Identity?.IsAuthenticated == true)
            {
                _logger.LogInformation("User already authenticated. Skipping token validation.");
                await _next(context);
                return;
            }

            // Lấy token từ cookies
            var token = context.Request.Cookies["accessToken"];
            _logger.LogInformation("Checking token in cookies...");

            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    // Tạo HttpClient và thêm token vào header để xác thực cho các yêu cầu HTTP
                    var client = _httpClientFactory.CreateClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    _logger.LogInformation("Token found and added to HTTP headers.");

                    // Giải mã token và tạo claims
                    var claims = JwtParser.ParseClaimsFromJwt(token);
                    var identity = new ClaimsIdentity(claims, "jwtAuthType");

                    // Cập nhật thông tin người dùng trong HttpContext
                    context.User = new ClaimsPrincipal(identity);

                    _logger.LogInformation("User authenticated successfully via token.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to decode JWT token or set claims. Clearing user session.");
                    // Nếu có lỗi, đặt người dùng thành ẩn danh
                    context.User = new ClaimsPrincipal(new ClaimsIdentity());

                    // Tùy chọn: xóa token nếu không hợp lệ để tránh lỗi liên tục
                    context.Response.Cookies.Delete("accessToken");
                }
            }
            else
            {
                _logger.LogInformation("No token found in cookies. Proceeding as unauthenticated.");
                context.User = new ClaimsPrincipal(new ClaimsIdentity()); // Đặt người dùng ẩn danh nếu không có token
            }

            // Tiếp tục pipeline request
            await _next(context);
        }
    }
}
