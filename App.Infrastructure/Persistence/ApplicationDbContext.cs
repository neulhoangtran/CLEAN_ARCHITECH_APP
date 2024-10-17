using Microsoft.EntityFrameworkCore;
using App.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Security.Cryptography;

namespace App.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        // Định nghĩa các DbSet cho các thực thể
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Token> Tokens { get; set; }

        // Constructor nhận vào DbContextOptions để cấu hình kết nối
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Thiết lập giá trị mặc định cho CreatedAt và UpdatedAt cho tất cả các thực thể kế thừa từ BaseEntity
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    modelBuilder.Entity(entityType.ClrType)
                        .Property<DateTime>("CreatedAt")
                        .HasDefaultValueSql("GETUTCDATE()");

                    modelBuilder.Entity(entityType.ClrType)
                        .Property<DateTime>("UpdatedAt")
                        .HasDefaultValueSql("GETUTCDATE()");
                }
            }

            // Cấu hình cho thực thể User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.ID); // Đặt ID làm khóa chính

                entity.HasIndex(e => e.Username)
                      .IsUnique(); // Đảm bảo Username là duy nhất

                entity.Property(e => e.Username)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Email)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.PasswordHash)
                      .IsRequired();

                entity.Property(e => e.Status)
                      .HasDefaultValue(UserStatus.Active);

                // Thiết lập quan hệ giữa User và UserProfile (1-1)
                entity.HasOne(e => e.UserProfile)
                      .WithOne(up => up.User)
                      .HasForeignKey<UserProfile>(e => e.UserID)
                      .OnDelete(DeleteBehavior.Cascade); // Xóa User sẽ xóa luôn UserProfile
            });

            // Cấu hình cho thực thể UserProfile
            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.ToTable("User_Profile");
                entity.HasKey(e => e.ID);

                entity.Property(e => e.UserID)
                      .IsRequired();

                entity.Property(e => e.FullName)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(e => e.Address)
                      .HasMaxLength(300);
            });

            // Cấu hình cho thực thể Token
            modelBuilder.Entity<Token>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.Property(e => e.TokenValue)
                      .IsRequired()
                      .HasMaxLength(500);

                entity.Property(e => e.TokenType)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.Expiration)
                      .IsRequired();

                entity.HasOne(e => e.User)
                      .WithMany(u => u.Tokens)
                      .HasForeignKey(e => e.UserID)
                      .OnDelete(DeleteBehavior.Cascade); // Xóa User sẽ xóa luôn Tokens liên quan
            });

            // Cấu hình cho thực thể Permission
            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.HasIndex(e => e.PermissionName)
                      .IsUnique();

                entity.Property(e => e.PermissionName)
                      .IsRequired()
                      .HasMaxLength(100);
            });

            // Cấu hình cho thực thể Role
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.HasIndex(e => e.RoleName)
                      .IsUnique();
            });

            // Cấu hình cho thực thể RolePermission
            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.ToTable("Role_Permission");
                entity.HasKey(rp => new { rp.RoleID, rp.PermissionID });

                entity.HasOne(rp => rp.Role)
                      .WithMany(r => r.RolePermissions)
                      .HasForeignKey(rp => rp.RoleID)
                      .OnDelete(DeleteBehavior.Cascade); // Xóa Role sẽ xóa luôn RolePermissions liên quan

                entity.HasOne(rp => rp.Permission)
                      .WithMany(p => p.RolePermissions)
                      .HasForeignKey(rp => rp.PermissionID)
                      .OnDelete(DeleteBehavior.Cascade); // Xóa Permission sẽ xóa luôn RolePermissions liên quan
            });

            // Cấu hình cho thực thể UserRole
            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("Role_User");
                entity.HasKey(ur => new { ur.UserID, ur.RoleID });

                entity.HasOne(ur => ur.User)
                      .WithMany(u => u.UserRoles)
                      .HasForeignKey(ur => ur.UserID)
                      .OnDelete(DeleteBehavior.Cascade); // Xóa User sẽ xóa luôn UserRoles liên quan

                entity.HasOne(ur => ur.Role)
                      .WithMany(r => r.UserRoles)
                      .HasForeignKey(ur => ur.RoleID)
                      .OnDelete(DeleteBehavior.Cascade); // Xóa Role sẽ xóa luôn UserRoles liên quan
            });


            // Tạo mật khẩu băm
            string hashedPassword = HashPassword("Admin123");

            // Tạo role mặc định
            var adminRole = new Role
            {
                ID = 1, // Đặt ID cố định
                RoleName = "Administrator"
            };

            // Tạo role mặc định
            var employeeRole = new Role
            {
                ID = 2, // Đặt ID cố định
                RoleName = "Employee"
            };

            // Tạo user mặc định
            var adminUser = new User
            {
                ID = 1,
                Username = "admin",
                EmployeeId = "ADMIN001",
                Email = "admin@example.com",
                PasswordHash = hashedPassword,
                Status = UserStatus.Active
            };

            // Tạo quan hệ user-role mặc định
            var userRole = new UserRole
            {
                UserID = 1,
                RoleID = 1
            };

            // Seed data vào cơ sở dữ liệu
            modelBuilder.Entity<Role>().HasData(adminRole);
            modelBuilder.Entity<Role>().HasData(employeeRole);
            modelBuilder.Entity<User>().HasData(adminUser);
            modelBuilder.Entity<UserRole>().HasData(userRole);


            // Seed permissions for User group
            modelBuilder.Entity<Permission>().HasData(
                new Permission { ID = 1, PermissionName = "User_View", Description = "View user list", Group = "User", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Permission { ID = 2, PermissionName = "User_Add", Description = "Add new user", Group = "User", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Permission { ID = 3, PermissionName = "User_Edit", Description = "Edit user information", Group = "User", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Permission { ID = 4, PermissionName = "User_Delete", Description = "Delete user", Group = "User", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            );

            // Seed permissions for Role group
            modelBuilder.Entity<Permission>().HasData(
                new Permission { ID = 5, PermissionName = "Role_Add", Description = "Add new role", Group = "Role", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Permission { ID = 6, PermissionName = "Role_Edit", Description = "Edit role", Group = "Role", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Permission { ID = 7, PermissionName = "Role_Delete", Description = "Delete role", Group = "Role", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            );

            // Seed permissions for Checklist group
            modelBuilder.Entity<Permission>().HasData(
                new Permission { ID = 8, PermissionName = "Checklist_View", Description = "View checklist list", Group = "Checklist", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Permission { ID = 9, PermissionName = "Checklist_AssignShift", Description = "Assign employees to shifts", Group = "Checklist", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Permission { ID = 10, PermissionName = "Checklist_ConfirmShift", Description = "Confirm shifts", Group = "Checklist", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            );

            // Seed permissions for Report group
            modelBuilder.Entity<Permission>().HasData(
                new Permission { ID = 11, PermissionName = "Report_View", Description = "View reports", Group = "Report", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            );

            // Seed permissions for DailyLog group
            modelBuilder.Entity<Permission>().HasData(
                new Permission { ID = 12, PermissionName = "DailyLog_View", Description = "View daily logs", Group = "DailyLog", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            );

            // Seed permissions for Settings group
            modelBuilder.Entity<Permission>().HasData(
                new Permission { ID = 13, PermissionName = "Settings_Modify", Description = "Modify system settings", Group = "Settings", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            );
        }

        // Hàm băm mật khẩu (có thể dùng các thuật toán băm như SHA-256)
        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        // Override phương thức SaveChangesAsync để tự động cập nhật UpdatedAt
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
