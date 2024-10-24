using BlazorWebAssembly.Models.Permission;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Linq;
using System.Net;

namespace BlazorWebAssembly.Services
{
    public class PermissionService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PermissionService> _logger;

        public PermissionService(HttpClient httpClient, ILogger<PermissionService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        // Phương thức lấy tất cả danh mục và quyền của danh mục từ API
        public async Task<List<PermissionGroupModel>> GetPermissionsGroupedByCategoryAsync()
        {
            _logger.LogInformation("Fetching permissions grouped by category from API.");

            try
            {
                var response = await _httpClient.GetAsync("api/permission");

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _logger.LogWarning("Received 401 Unauthorized response.");
                    // Trả về danh sách rỗng nếu không có quyền truy cập
                    return new List<PermissionGroupModel>();
                }

                response.EnsureSuccessStatusCode();
                var categories = await response.Content.ReadFromJsonAsync<List<PermissionGroupModel>>();

                // Kiểm tra nếu categories là null thì trả về danh sách rỗng
                if (categories == null)
                {
                    _logger.LogWarning("No categories were returned from the API.");
                    return new List<PermissionGroupModel>();
                }

                // Sắp xếp danh mục theo order (categoryOrder)
                return categories.OrderBy(c => c.Order).ToList();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error occurred while fetching permissions from API.");
                // Trả về danh sách rỗng khi gặp lỗi
                return new List<PermissionGroupModel>();
            }
        }


        // Phương thức lấy quyền theo ID từ API
        public async Task<PermissionModel> GetPermissionByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/permission/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PermissionModel>();
        }

        // Phương thức cập nhật quyền qua API
        public async Task UpdatePermissionAsync(int id, PermissionModel updatedPermission)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/permission/{id}", updatedPermission);
            response.EnsureSuccessStatusCode();
        }

        // Phương thức xóa quyền qua API
        public async Task DeletePermissionAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/permission/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
