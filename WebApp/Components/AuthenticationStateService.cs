using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace WebApp.Components
{
    public class AuthenticationStateService : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _httpClient;
        private readonly AuthenticationState _anonymous;

        public AuthenticationStateService(ILocalStorageService localStorage, HttpClient httpClient)
        {
            _localStorage = localStorage;
            _httpClient = httpClient;
            _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("accessToken");
            if (string.IsNullOrEmpty(token))
            {
                return _anonymous;
            }

            // Thêm token vào header để xác thực cho các yêu cầu HTTP
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var claims = JwtParser.ParseClaimsFromJwt(token);
            var identity = new ClaimsIdentity(claims, "jwtAuthType");

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        public async Task LoginAsync(string token)
        {
            await _localStorage.SetItemAsync("accessToken", token);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task LogoutAsync()
        {
            await _localStorage.RemoveItemAsync("accessToken");
            _httpClient.DefaultRequestHeaders.Authorization = null;
            NotifyAuthenticationStateChanged(Task.FromResult(_anonymous));
        }

        public async Task<bool> IsUserLoggedIn()
        {
            var token = await _localStorage.GetItemAsync<string>("accessToken");
            return !string.IsNullOrEmpty(token);
        }
    }
}
