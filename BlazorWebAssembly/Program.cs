using BlazorWebAssembly;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorWebAssembly.Services;
using Blazored.LocalStorage;
using BlazorWebAssembly.DI;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Bật hiển thị thông tin chi tiết (PII) cho các lỗi JWT
Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;

//đăng ký Các Service để có thể sử dụng ở mọi nơi trong ứng dụng.
builder.Services.AddScoped<LayoutService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<RoleService>();

// Đăng ký dịch vụ Blazored.LocalStorage
builder.Services.AddBlazoredLocalStorage();

// Đăng ký dịch vụ AuthorizationCore (chỉ cho Blazor WebAssembly)
builder.Services.AddAuthorizationCore();

// Đăng ký AuthenticationStateProvider (đây là dịch vụ quan trọng cho xác thực)
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationStateService>();

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
// Đọc cấu hình từ appsettings.json
//var apiUrl = builder.Configuration.GetValue<string>("ApiSettings:API_URL");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7277/") });



builder.Services.AddScoped<BearerTokenHandler>();

builder.Services.AddHttpClient<RoleService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7277/"); // Replace with your API base URL
})
.AddHttpMessageHandler<BearerTokenHandler>();

builder.Services.AddHttpClient<UserService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7277/"); // Replace with your API base URL
})
.AddHttpMessageHandler<BearerTokenHandler>();

// Cấu hình HttpClient sử dụng URL từ appsettings.json
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiUrl) });


await builder.Build().RunAsync();
