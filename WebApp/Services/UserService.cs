using System.Net.Http.Json;
using WebApp.Models.User;
namespace WebApp.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserListResponse> GetUsersAsync(int pageIndex, int pageSize)
        {
            var response = await _httpClient.GetFromJsonAsync<UserListResponse>($"api/user/list?pageIndex={pageIndex}&pageSize={pageSize}");
            return response;
        }
    }
}
