using System.Net;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
namespace WebApp.Components
{
    public class TokenExpirationHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<TokenExpirationHandler> _logger;

        public TokenExpirationHandler(IHttpContextAccessor httpContextAccessor, ILogger<TokenExpirationHandler> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Gửi yêu cầu HTTP ban đầu
            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Nếu mã trạng thái là 401 Unauthorized
                _logger.LogInformation("Token expired or invalid. Removing token cookie and logging out.");

                // Xóa token từ cookies
                _httpContextAccessor.HttpContext?.Response.Cookies.Delete("accessToken");

                // Tùy chọn: Thực hiện hành động đăng xuất, ví dụ, điều hướng người dùng đến trang đăng nhập
                // Nếu bạn đang làm việc với Blazor Server hoặc Razor Pages:
                _httpContextAccessor.HttpContext?.Response.Redirect("/login");

                // Tạo phản hồi giả khi token không hợp lệ
                return new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent("Unauthorized. Token has expired.")
                };
            }

            // Trả về phản hồi bình thường nếu không phải lỗi 401
            return response;
        }
    }
}
