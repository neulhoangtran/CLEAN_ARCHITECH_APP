namespace WebApp.Middlewares
{
    public class SaveTokenMiddleware
    {
        private readonly RequestDelegate _next;

        public SaveTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Kiểm tra nếu request có chứa thông tin đăng nhập
            if (context.Request.Path == "/login" && context.Response.StatusCode == 200)
            {
                // Lấy token từ response (giả sử token được trả về từ login response)
                var token = context.Request.Headers["Authorization"].ToString();

                if (!string.IsNullOrEmpty(token))
                {
                    // Lưu token vào cookie
                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        Expires = DateTimeOffset.Now.AddDays(7)
                    };
                    context.Response.Cookies.Append("accessToken", token, cookieOptions);
                }
            }

            // Tiếp tục pipeline request
            await _next(context);
        }
    }

}
