using System.Net.Http.Json;
using WebApp.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using WebApp.Models;

namespace WebApp.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<UserService> _logger;

        public UserService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, ILogger<UserService> logger)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        // Login function to handle login requests
        public async Task<LoginResponse> LoginAsync(string username, string password)
        {
            _logger.LogInformation("Sending login request to API.");

            var loginPayload = new { Username = username, Password = password };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/auth/login", loginPayload);
                _logger.LogInformation("Received response from login API.");

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _logger.LogWarning("Login failed with 401 Unauthorized.");
                    return new LoginResponse { Success = false, Message = "Unauthorized. Please check your credentials." };
                }

                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

                if (result != null && !string.IsNullOrEmpty(result.Token.AccessToken))
                {
                    
                    return new LoginResponse { Success = true, Token = result.Token };
                }
                else
                {
                    _logger.LogWarning("Login failed. Invalid response from server.");
                    return new LoginResponse { Success = false, Message = "Invalid login response" };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during login.");
                return new LoginResponse { Success = false, Message = $"Login error: {ex.Message}" };
            }
        }

        // Get users function to handle fetching user data
        public async Task<UserListResponse> GetUsersAsync(int pageIndex, int pageSize)
        {
            _logger.LogInformation("Sending request to API to fetch users.");

            try
            {
                var response = await _httpClient.GetAsync($"api/user/list?pageIndex={pageIndex}&pageSize={pageSize}");
                _logger.LogInformation("Received response from API.");

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _logger.LogWarning("Received 401 Unauthorized response. Removing token.");

                    // Remove token from cookies
                    _httpContextAccessor.HttpContext.Response.Cookies.Delete("accessToken");

                    // Optionally: redirect to login page or handle unauthorized state
                    return null;
                }

                response.EnsureSuccessStatusCode();
                _logger.LogInformation("API request succeeded.");
                return await response.Content.ReadFromJsonAsync<UserListResponse>();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error occurred while fetching users from API.");
                return null;
            }
        }

        // Method to asynchronously remove the token from the cookies
        public async Task RemoveTokenAsync()
        {
            _logger.LogInformation("Attempting to remove access token from cookies.");

            // Check if the access token exists and remove it
            if (_httpContextAccessor.HttpContext?.Request.Cookies["accessToken"] != null)
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Delete("accessToken");
                _logger.LogInformation("Access token removed successfully.");
            }
            else
            {
                _logger.LogWarning("No access token found in cookies.");
            }

            await Task.CompletedTask;
        }
    }
}
