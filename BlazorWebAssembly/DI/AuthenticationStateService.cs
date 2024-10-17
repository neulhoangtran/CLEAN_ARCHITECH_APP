using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.Extensions.Logging;

namespace BlazorWebAssembly.DI
{
    public class AuthenticationStateService : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly AuthenticationState _anonymous;
        private readonly ILogger<AuthenticationStateService> _logger;
        private readonly NavigationManager _navigationManager;
        private const string TokenKey = "authToken";

        public AuthenticationStateService(ILocalStorageService localStorageService, ILogger<AuthenticationStateService> logger, NavigationManager navigationManager)
        {
            _localStorageService = localStorageService;
            _logger = logger;
            _navigationManager = navigationManager; // Gán giá trị
            _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            _logger.LogInformation("Attempting to retrieve authentication state.");

            //var token = await _localStorageService.GetItemAsStringAsync("accessToken");
            var token = (await _localStorageService.GetItemAsStringAsync(TokenKey))?.Trim().Trim('"');

            _logger.LogInformation(token);
            if (string.IsNullOrWhiteSpace(token))
            {
                _logger.LogWarning("No token found. User is not authenticated.");
                return _anonymous;
            }

            _logger.LogInformation("Token found. Parsing claims.");

            try
            {
                var claims = JwtParser.ParseClaimsFromJwt(token);
                var identity = new ClaimsIdentity(claims, "jwtAuthType");
                var user = new ClaimsPrincipal(identity);

                _logger.LogInformation("User is authenticated.");
                return new AuthenticationState(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error parsing token claims.");
                return _anonymous;
            }
        }

        public async Task MarkUserAsAuthenticated(string token)
        {
            _logger.LogInformation("Marking user as authenticated.");
            await _localStorageService.SetItemAsync(TokenKey, token);
            var claims = JwtParser.ParseClaimsFromJwt(token);
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwtAuthType"));
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(authenticatedUser)));
            _logger.LogInformation("User authenticated and state updated.");
        }

        public async Task MarkUserAsLoggedOut()
        {
            _logger.LogInformation("Logging out user.");
            await _localStorageService.RemoveItemAsync(TokenKey);
            NotifyAuthenticationStateChanged(Task.FromResult(_anonymous));
            _navigationManager.NavigateTo(_navigationManager.Uri, forceLoad: true);
            _logger.LogInformation("User logged out and state updated.");
        }
    }
}
