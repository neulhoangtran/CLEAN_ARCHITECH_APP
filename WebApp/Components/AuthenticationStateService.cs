using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using WebApp.Models;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace WebApp.Components
{
    public class AuthenticationStateService : AuthenticationStateProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;
        private readonly AuthenticationState _anonymous;
        private readonly ILogger<AuthenticationStateService> _logger;
        private readonly IJSRuntime _jsRuntime;

        public AuthenticationStateService(IHttpContextAccessor httpContextAccessor, HttpClient httpClient, IJSRuntime jsRuntime, ILogger<AuthenticationStateService> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _logger = logger; // Khởi tạo logger
            _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // Lấy token từ cookies
            var token = _httpContextAccessor.HttpContext?.Request.Cookies["accessToken"];
            if (string.IsNullOrEmpty(token))
            {
                return Task.FromResult(_anonymous);
            }
            _logger.LogInformation("test --- 1111");
            // Thêm token vào header để xác thực cho các yêu cầu HTTP
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Giải mã token và tạo claims
            var claims = JwtParser.ParseClaimsFromJwt(token);
            var identity = new ClaimsIdentity(claims, "jwtAuthType");

            return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
        }

        public async Task<LoginResponse> LoginAsync(string username, string password)
        {
            try
            {
                // Tạo payload để gửi yêu cầu đăng nhập
                var loginModel = new { Username = username, Password = password };
                var response = await _httpClient.PostAsJsonAsync("/api/auth/login", loginModel);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                    if (result != null && !string.IsNullOrEmpty(result.Token.AccessToken))
                    {
                        
                        // Thêm token vào cookie
                        await _jsRuntime.InvokeVoidAsync("setCookie", "accessToken", result.Token.AccessToken, 7);
                        //_httpContextAccessor.HttpContext.Response.Cookies.Append("accessToken", result.Token.AccessToken, cookieOptions);

                        // Thêm token vào header cho các yêu cầu HTTP tiếp theo
                        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Token.AccessToken);

                        // Cập nhật trạng thái xác thực
                        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

                        return new LoginResponse { Success = true, Message = "Login successful" };
                    }
                    else
                    {
                        return new LoginResponse { Success = false, Message = "Invalid login response" };
                    }
                }
                else
                {
                    var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
                    return new LoginResponse { Success = false, Message = error?.Message ?? "Login failed." };
                }
            }
            catch (Exception ex)
            {
                return new LoginResponse { Success = false, Message = $"Login error: {ex.Message}" };
            }
        }

        public async Task LogoutAsync()
        {
            if (_httpContextAccessor.HttpContext.Response.HasStarted)
            {
                throw new InvalidOperationException("Cannot modify cookies after response has started.");
            }

            // Xóa token từ cookies
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("accessToken");

            // Xóa token khỏi header
            _httpClient.DefaultRequestHeaders.Authorization = null;

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()))));
        }

        public bool IsUserLoggedIn()
        {
            var token = _httpContextAccessor.HttpContext.Request.Cookies["accessToken"];
            return !string.IsNullOrEmpty(token);
        }
    }
}
