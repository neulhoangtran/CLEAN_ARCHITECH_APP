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
using App.Domain.Events;
using App.Infrastructure.Events;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

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

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>(); // Đăng ký TokenService
            services.AddScoped<ITokenRepository, TokenRepository>(); // Đăng ký TokenRepository

            // Đăng ký IRoleService
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IRoleRepository, RoleRepository>();

            // Đăng ký Permission 
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IPermissionService, PermissionService>();

            // Đăng ký EventBus và Event Handlers
            services.AddSingleton<IEventBus, InMemoryEventBus>();   // Đăng ký InMemoryEventBus
            services.AddTransient<UserRegisteredEventHandler>();    // Đăng ký UserRegisteredEventHandler
            services.AddTransient<UserDeletedEventHandler>();       // Đăng ký UserDeletedEventHandler


            // Đăng ký Controllers
            services.AddControllers();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });

            // Cấu hình Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MySolution API", Version = "v1" });

                // Cấu hình bảo mật cho Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your token in the text input below.\n\nExample: \"Bearer 12345abcdef\""
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
            //services.AddAuthorization();
            });
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "APP API V1");
            });

            app.UseHttpsRedirection();
            app.UseRouting();

            // Bật xác thực JWT
            app.UseAuthentication();
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
