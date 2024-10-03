using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace WebApp.Components
{
    public class AuthenticationStateService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly NavigationManager _navigationManager;
		private bool _isPrerendering = true;

		public AuthenticationStateService(ILocalStorageService localStorage, NavigationManager navigationManager)
        {
            _localStorage = localStorage;
            _navigationManager = navigationManager;
        }

        public async Task<bool> IsUserLoggedIn()
        {
			if (_isPrerendering)
			{
				return false; // Trong quá trình prerendering, chưa thể gọi JavaScript
			}
			var accessToken = await _localStorage.GetItemAsync<string>("accessToken");
            return !string.IsNullOrEmpty(accessToken);
        }

        // Đảm bảo người dùng đã đăng nhập và điều hướng đến home nếu cần
        public async Task EnsureUserIsLoggedIn()
        {
            var isLoggedIn = await IsUserLoggedIn();
            var currentUri = _navigationManager.Uri;
            var isOnLoginPage = currentUri.Contains("/login");

            if (isLoggedIn && isOnLoginPage)
            {
                // Nếu đã đăng nhập và đang ở trang login, điều hướng đến trang home
                _navigationManager.NavigateTo("/");
            }
            else if (!isLoggedIn && !isOnLoginPage)
            {
                // Nếu chưa đăng nhập và không phải ở trang login, điều hướng đến trang login
                _navigationManager.NavigateTo("/login");
            }
        }

        public async Task LogOut()
        {
			// Xóa token khi đăng xuất
			if (!_isPrerendering)
			{
				await _localStorage.RemoveItemAsync("accessToken");
				await _localStorage.RemoveItemAsync("refreshToken");
				_navigationManager.NavigateTo("/login");
			}
		}
		public void SetPrerenderingState(bool isPrerendering)
		{
			_isPrerendering = isPrerendering;
		}
	}
}
