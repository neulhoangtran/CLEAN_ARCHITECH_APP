using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using WebApp.Models;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace WebApp.Services
{
    public class AuthenticationStateService : AuthenticationStateProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationState _anonymous;
        private readonly ILogger<AuthenticationStateService> _logger;

        public AuthenticationStateService(IHttpContextAccessor httpContextAccessor, ILogger<AuthenticationStateService> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            _logger = logger;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null || !user.Identity.IsAuthenticated)
            {
                _logger.LogInformation("User is not authenticated.");
                return Task.FromResult(_anonymous);
            }

            _logger.LogInformation("User is authenticated.");
            return Task.FromResult(new AuthenticationState(user));
        }
    }

}
