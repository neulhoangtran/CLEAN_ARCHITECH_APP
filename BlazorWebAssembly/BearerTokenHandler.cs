using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace BlazorWebAssembly
{
    public class BearerTokenHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorage;

        public BearerTokenHandler(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Retrieve the token from localStorage
            var token = await _localStorage.GetItemAsync<string>("accessToken");

            if (!string.IsNullOrEmpty(token))
            {
                // Add the Bearer token to the Authorization header
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
