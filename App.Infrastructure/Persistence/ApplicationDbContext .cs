using Microsoft.EntityFrameworkCore;
using App.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace App.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        // Định nghĩa các DbSet cho các thực thể
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        // Constructor nhận vào DbContextOptions để cấu hình kết nối
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Cấu hình các thực thể trong phương thức OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Thiết lập giá trị mặc định cho CreatedAt và UpdatedAt cho tất cả các thực thể kế thừa từ BaseEntity
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    // Cấu hình ID tự tăng
                    modelBuilder.Entity(entityType.ClrType)
                        .Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    // Thiết lập giá trị mặc định cho CreatedAt
                    modelBuilder.Entity(entityType.ClrType)
                        .Property<DateTime>("CreatedAt")
                        .HasDefaultValueSql("GETUTCDATE()"); // Sử dụng GETUTCDATE() cho thời gian hiện tại UTC

                    // Thiết lập giá trị mặc định cho UpdatedAt
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

                entity.Property(e => e.Email)
                      .IsRequired()
                      .HasMaxLength(100); // Đặt điều kiện cho cột Email

                // Thiết lập quan hệ giữa User và UserProfile (1-1)
                entity.HasOne(e => e.UserProfile)
                      .WithOne(up => up.User)
                      .HasForeignKey<UserProfile>(e => e.UserID); // UserProfile sẽ có khóa ngoại UserID
            });

            // Cấu hình cho thực thể Role
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.ID); // Đặt ID làm khóa chính

                entity.HasIndex(e => e.Name)
                      .IsUnique(); // Đảm bảo tên Role là duy nhất
            });

            // Cấu hình cho thực thể UserProfile
            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.ToTable("User_Profile");
                entity.HasKey(e => e.ID); // Đặt ID làm khóa chính

                entity.Property(e => e.FullName)
                      .IsRequired()
                      .HasMaxLength(200); // Đặt điều kiện cho cột FullName

                entity.Property(e => e.Address)
                      .HasMaxLength(300); // Đặt độ dài tối đa cho cột Address
            });

            // Cấu hình cho thực thể Token
            modelBuilder.Entity<Token>(entity =>
            {
                entity.HasKey(e => e.ID); // Đặt ID làm khóa chính

                entity.Property(e => e.TokenValue)
                      .IsRequired()
                      .HasMaxLength(500); // Đặt điều kiện cho cột TokenValue

                entity.Property(e => e.TokenType)
                      .IsRequired()
                      .HasMaxLength(50); // Đặt điều kiện cho cột TokenType

                entity.Property(e => e.Expiration)
                      .IsRequired(); // Đặt điều kiện cho cột Expiration

                entity.HasOne(e => e.User)
                      .WithMany(u => u.Tokens)
                      .HasForeignKey(e => e.UserId); // Khóa ngoại liên kết với bảng User
            });

            // Cấu hình cho thực thể Permission
            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasKey(e => e.ID); // Đặt ID làm khóa chính

                entity.HasIndex(e => e.PermissionName)
                      .IsUnique(); // Đảm bảo tên quyền hạn là duy nhất

                entity.Property(e => e.PermissionName)
                      .IsRequired()
                      .HasMaxLength(100); // Đặt điều kiện cho cột PermissionName
            });

            // Cấu hình cho thực thể RolePermission
            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.ToTable("Role_Permission");
                entity.HasKey(rp => new { rp.RoleID, rp.PermissionID }); // Composite Key

                entity.HasOne(rp => rp.Role)
                      .WithMany(r => r.RolePermissions)
                      .HasForeignKey(rp => rp.RoleID);

                entity.HasOne(rp => rp.Permission)
                      .WithMany(p => p.RolePermissions)
                      .HasForeignKey(rp => rp.PermissionID);
            });

            // Cấu hình cho thực thể UserRole
            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("Role_User");
                entity.HasKey(ur => new { ur.UserID, ur.RoleID }); // Composite Key

                entity.HasOne(ur => ur.User)
                      .WithMany(u => u.UserRoles)
                      .HasForeignKey(ur => ur.UserID);

                entity.HasOne(ur => ur.Role)
                      .WithMany(r => r.UserRoles)
                      .HasForeignKey(ur => ur.RoleID);
            });
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
