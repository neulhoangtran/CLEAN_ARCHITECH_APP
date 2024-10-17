using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using BlazorWebAssembly.DI;
public class BearerTokenHandler : DelegatingHandler
{
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly ILogger<BearerTokenHandler> _logger;

    public BearerTokenHandler(ILocalStorageService localStorage,
                              AuthenticationStateProvider authenticationStateProvider,
                              ILogger<BearerTokenHandler> logger)
    {
        _localStorage = localStorage;
        _authenticationStateProvider = authenticationStateProvider;
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Retrieve the token from localStorage
        var token = await _localStorage.GetItemAsync<string>("accessToken");

        if (!string.IsNullOrEmpty(token))
        {
            // Log adding token to request
            _logger.LogInformation("Adding Bearer token to Authorization header.");

            // Add the Bearer token to the Authorization header
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        var response = await base.SendAsync(request, cancellationToken);


        // Check for 401 Unauthorized
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            // Log the unauthorized response
            _logger.LogWarning("Received 401 Unauthorized. Logging out the user and removing token.");

            // Remove the token from localStorage and log out the user
            if (_authenticationStateProvider is AuthenticationStateService authStateService)
            {
                authStateService.MarkUserAsLoggedOut();
            }
        }

        return response;
    }
}
