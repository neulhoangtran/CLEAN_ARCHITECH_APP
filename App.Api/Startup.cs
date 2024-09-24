using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using App.Application.Interfaces;
using App.Application.Services;
using App.Application.Handlers;
using App.Domain.Repositories;
using App.Domain.Services;
using App.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace App.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Đăng ký các dịch vụ
        public void ConfigureServices(IServiceCollection services)
        {
            // Đăng ký ApplicationDbContext với Entity Framework Core
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Đăng ký các dịch vụ của Domain Layer
            services.AddScoped<IUserRepository, UserRepository>();  // Đăng ký UserRepository
            services.AddScoped<IRoleRepository, RoleRepository>();  // Đăng ký RoleRepository
            services.AddScoped<UserDomainService>();                // Đăng ký UserDomainService

            // Đăng ký các dịch vụ của Application Layer
            services.AddScoped<IUserService, UserService>();        // Đăng ký UserService
            services.AddScoped<IEmailService, EmailService>();      // Đăng ký EmailService

            // Đăng ký EventBus và Event Handlers
            services.AddSingleton<IEventBus, InMemoryEventBus>();   // Đăng ký InMemoryEventBus
            services.AddTransient<UserRegisteredEventHandler>();    // Đăng ký UserRegisteredEventHandler
            services.AddTransient<UserDeletedEventHandler>();       // Đăng ký UserDeletedEventHandler

            // Đăng ký Controllers
            services.AddControllers();

            // Cấu hình Swagger (nếu có)
            services.AddSwaggerGen();
        }

        // Cấu hình pipeline xử lý HTTP requests
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IEventBus eventBus)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Cấu hình Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MySolution API V1");
            });

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Đăng ký các Event Handlers với EventBus
            //eventBus.Subscribe<UserRegisteredEvent, UserRegisteredEventHandler>();
            //eventBus.Subscribe<UserDeletedEvent, UserDeletedEventHandler>();
        }
    }
}
