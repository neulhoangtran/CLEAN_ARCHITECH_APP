using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using System.Net;
using BlazorWebAssembly.Models;
using Blazored.LocalStorage;
using System.Net.Http.Headers;
using BlazorWebAssembly.Models.Role;
namespace BlazorWebAssembly.Services
{
    public class RoleService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly ILogger<RoleService> _logger;

        public RoleService(HttpClient httpClient, ILocalStorageService localStorage, ILogger<RoleService> logger)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _logger = logger;
        }

        // Function to fetch the list of roles
        public async Task<RoleListResponse> GetRolesAsync(int pageIndex, int pageSize, string sortBy = null, string filter = null)
        {
            _logger.LogInformation("Sending request to API to fetch roles.");

            try
            {

                // Construct the URL for pagination, sorting, and filtering
                var url = $"api/role/list?pageIndex={pageIndex}&pageSize={pageSize}";

                // Add sorting to the URL if applicable
                if (!string.IsNullOrEmpty(sortBy))
                {
                    url += $"&sortBy={sortBy}";
                }

                // Add filtering to the URL if applicable
                if (!string.IsNullOrEmpty(filter))
                {
                    url += $"&filter={filter}";
                }

                var response = await _httpClient.GetAsync(url);
                _logger.LogInformation("Received response from API.");

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _logger.LogWarning("Received 401 Unauthorized response. Removing token from localStorage.");
                    await RemoveTokenAsync(); // Remove token if unauthorized
                    return null;
                }

                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<RoleListResponse>();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error occurred while fetching roles from API.");
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


        // Phương thức lấy thông tin role theo ID
        public async Task<RoleModel> GetRoleByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/api/role/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<RoleModel>();
        }

        // Phương thức cập nhật role
        public async Task UpdateRoleAsync(int id, RoleModel role)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/role/{id}", role);
            response.EnsureSuccessStatusCode();
        }

        // Thêm người dùng vào role
        public async Task AddUserToRoleAsync(int roleId, int userId)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/role/{roleId}/add-user", new { UserId = userId });
            response.EnsureSuccessStatusCode();
        }
    }
}
