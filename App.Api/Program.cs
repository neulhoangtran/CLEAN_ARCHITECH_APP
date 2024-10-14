using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using App.Api; // Import namespace chứa Startup
using App.Domain.Events; // Import namespace chứa Startup

var builder = WebApplication.CreateBuilder(args);

// Cấu hình và gọi Startup
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Thêm CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", builder =>
    {
        // Định nghĩa URL của ứng dụng Blazor WebAssembly được phép truy cập API này
        builder.WithOrigins("http://localhost:7264") // URL của Blazor WebAssembly app
               .AllowAnyMethod()  // Cho phép tất cả các phương thức HTTP (GET, POST, PUT, DELETE, etc.)
               .AllowAnyHeader(); // Cho phép tất cả các header
    });
});


var startup = new Startup(builder.Configuration); // Khởi tạo Startup với Configuration
startup.ConfigureServices(builder.Services); // Gọi phương thức ConfigureServices từ Startup

var app = builder.Build();


// Sử dụng CORS đã cấu hình ở trên
app.UseCors("AllowBlazorClient");

// Gọi phương thức Configure từ Startup
var serviceScope = app.Services.CreateScope(); // Tạo scope để truy cập dịch vụ
var eventBus = serviceScope.ServiceProvider.GetService<IEventBus>(); // Lấy EventBus từ DI Container
startup.Configure(app, app.Environment, eventBus);

app.Run();
