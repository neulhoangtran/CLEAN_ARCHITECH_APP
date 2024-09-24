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

var startup = new Startup(builder.Configuration); // Khởi tạo Startup với Configuration
startup.ConfigureServices(builder.Services); // Gọi phương thức ConfigureServices từ Startup

var app = builder.Build();

// Gọi phương thức Configure từ Startup
var serviceScope = app.Services.CreateScope(); // Tạo scope để truy cập dịch vụ
var eventBus = serviceScope.ServiceProvider.GetService<IEventBus>(); // Lấy EventBus từ DI Container
startup.Configure(app, app.Environment, eventBus);

app.Run();
