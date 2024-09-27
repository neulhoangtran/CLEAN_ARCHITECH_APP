# CLEAN_ARCHITECH_APP

dotnet ef migrations add InitialCreateDatabase -p App.Infrastructure/App.Infrastructure.csproj -s App.Api/App.Api.csproj

dotnet ef database update -p App.Infrastructure/App.Infrastructure.csproj -s App.Api/App.Api.csproj

<pre>
/MySolution.sln                       // Solution chính chứa tất cả các project
│
├── /src
│   ├── /MySolution.Domain            // Domain Layer: Chứa các entity và logic nghiệp vụ
│   │   ├── /Entities
│   │   │   ├── User.cs
│   │   │   ├── Log.cs
│   │   │   └── ...
│   │   ├── /ValueObjects
│   │   │   └── ...
│   │   ├── /Repositories             // Interface repository
│   │   │   ├── IUserRepository.cs
│   │   │   ├── ILogRepository.cs
│   │   │   └── ...
│   │   ├── /Events                   // Domain events
│   │   │   └── UserRegisteredEvent.cs
│   │   └── /Services                 // Domain services
│   │       └── UserDomainService.cs
│   │
│   ├── /MySolution.Application       // Application Layer: Chứa các service và logic ứng dụng
│   │   ├── /Interfaces               // Interface cho application service
│   │   │   ├── IUserService.cs
│   │   │   └── ...
│   │   ├── /DTOs                     // Các DTO dùng để truyền dữ liệu
│   │   │   ├── UserDto.cs
│   │   │   └── ...
│   │   ├── /Services                 // Implement các service của ứng dụng
│   │   │   ├── UserService.cs
│   │   │   └── ...
│   │   ├── /Commands                 // Các command trong CQRS
│   │   │   └── ...
│   │   ├── /Queries                  // Các query trong CQRS
│   │   │   └── ...
│   │   └── /Mappers                  // Mapper để chuyển đổi giữa entity và DTO
│   │       └── UserMapper.cs
│   │
│   ├── /MySolution.Infrastructure    // Infrastructure Layer: Kết nối với cơ sở dữ liệu, các dịch vụ bên ngoài
│   │   ├── /Persistence              // Quản lý kết nối cơ sở dữ liệu
│   │   │   ├── ApplicationDbContext.cs
│   │   │   ├── UserRepository.cs
│   │   │   └── ...
│   │   ├── /Identity                 // Các logic xác thực và phân quyền
│   │   │   ├── AppUser.cs
│   │   │   ├── Role.cs
│   │   │   └── ...
│   │   ├── /Messaging                // Event bus và các dịch vụ messaging khác
│   │   │   ├── EventBus.cs
│   │   │   └── ...
│   │   ├── /Services                 // Các dịch vụ bên ngoài như email, push notification
│   │   │   ├── EmailService.cs
│   │   │   └── ...
│   │   └── /Configurations           // Các file cấu hình của infrastructure
│   │       └── ...
│   │
│   ├── /MySolution.Api               // API Layer: Tách riêng, cung cấp endpoint cho web hoặc các ứng dụng khác
│   │   ├── /Controllers              // Các API controller
│   │   │   ├── UserController.cs
│   │   │   ├── AuthController.cs
│   │   │   └── ...
│   │   ├── /Middlewares              // Các middleware xử lý request
│   │   │   └── ExceptionMiddleware.cs
│   │   ├── /Models                   // Các model cho API request/response
│   │   │   └── RegisterUserRequest.cs
│   │   └── /Configurations           // Cấu hình cho API (Swagger, versioning, etc.)
│   │       ├── SwaggerConfig.cs
│   │       └── ...
│   │
│   ├── /MySolution.WebApp            // Web Application Layer: Giao diện người dùng cho ứng dụng web
│   │   ├── /Controllers              // Các MVC Controller cho trang web (nếu có)
│   │   ├── /Views                    // Các view cho ứng dụng web
│   │   │   ├── Home
│   │   │   ├── User
│   │   │   └── ...
│   │   └── /wwwroot                  // Static files (CSS, JS, Images)
│   │       └── ...
│   │
│   └── /MySolution.WindowsApp        // Windows Forms Application Layer: Ứng dụng dành cho Windows
│       ├── /Forms                    // Các form chính của ứng dụng
│       │   ├── MainForm.cs
│       │   ├── UserForm.cs
│       │   └── ...
│       ├── /Services                 // Giao tiếp với API, xử lý nghiệp vụ trên Windows app
│       │   ├── ApiService.cs
│       │   └── ...
│       └── /Resources                // Các tài nguyên (hình ảnh, file, etc.) cho Windows app
│           └── ...
│
└── /tests                            // Các dự án test cho từng layer
    ├── /MySolution.Domain.Tests      // Unit test cho domain layer
    ├── /MySolution.Application.Tests // Unit test cho application layer
    ├── /MySolution.Infrastructure.Tests // Unit test cho infrastructure layer
    └── /MySolution.Api.Tests         // Integration test cho API layer

</pre>
