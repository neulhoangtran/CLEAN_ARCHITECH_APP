﻿using WebApp.Components;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình logging vào console
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddAuthorizationCore();
// Đăng ký dịch vụ AuthenticationStateService
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationStateService>();
builder.Services.AddScoped<AuthenticationStateService>();


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Đăng ký Blazored.LocalStorage
builder.Services.AddBlazoredLocalStorage();

// Thêm cấu hình Logging để theo dõi lỗi chi tiết hơn
builder.Logging.SetMinimumLevel(LogLevel.Debug);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["ApiSettings:API_URL"]) });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();




app.Run();