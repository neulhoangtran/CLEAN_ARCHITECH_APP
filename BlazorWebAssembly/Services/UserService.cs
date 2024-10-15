using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using System.Net;
using BlazorWebAssembly.Models;
using BlazorWebAssembly.Models.User;
using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace BlazorWebAssembly.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly ILogger<UserService> _logger;

        public UserService(HttpClient httpClient, ILocalStorageService localStorage, ILogger<UserService> logger)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
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

                if (result != null && !string.IsNullOrEmpty(result.Token?.AccessToken))
                {
                    // Save the token to localStorage
                    await _localStorage.SetItemAsync("accessToken", result.Token.AccessToken);
                    _logger.LogInformation("Login succeeded and token stored in localStorage.");

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
        public async Task<UserListResponse> GetUsersAsync(int pageIndex, int pageSize, string sortBy = null, string filter = null)
        {
            _logger.LogInformation("Sending request to API to fetch users.");

            try
            {
                var token = await _localStorage.GetItemAsync<string>("accessToken");
                if (!string.IsNullOrEmpty(token))
                {
                    if (_httpClient.DefaultRequestHeaders.Authorization == null || _httpClient.DefaultRequestHeaders.Authorization.Parameter != token)
                    {
                        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }
                }

                // Xây dựng URL để gửi yêu cầu với phân trang, sắp xếp, và lọc
                var url = $"api/user/list?pageIndex={pageIndex}&pageSize={pageSize}";

                // Nếu có tham số sắp xếp, thêm nó vào URL
                if (!string.IsNullOrEmpty(sortBy))
                {
                    url += $"&sortBy={sortBy}";
                }

                // Nếu có tham số lọc, thêm nó vào URL
                if (!string.IsNullOrEmpty(filter))
                {
                    url += $"&filter={filter}";
                }

                var response = await _httpClient.GetAsync(url);
                _logger.LogInformation("Received response from API.");

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _logger.LogWarning("Received 401 Unauthorized response. Removing token from localStorage.");
                    await RemoveTokenAsync();
                    return null;
                }

                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<UserListResponse>();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error occurred while fetching users from API.");
                return null;
            }
        }


        // Method to asynchronously remove the token from the localStorage
        public async Task RemoveTokenAsync()
        {
            _logger.LogInformation("Attempting to remove access token from localStorage.");

            // Remove the token from localStorage
            await _localStorage.RemoveItemAsync("accessToken");
            _logger.LogInformation("Access token removed successfully.");
        }
    }
}
