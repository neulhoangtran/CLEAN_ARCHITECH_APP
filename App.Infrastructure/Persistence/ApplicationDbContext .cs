using Microsoft.EntityFrameworkCore;
using App.Domain.Entities;
using System.Data;

namespace App.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        // Định nghĩa các DbSet cho các thực thể
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        // Constructor nhận vào DbContextOptions để cấu hình kết nối
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Cấu hình các thực thể trong phương thức OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình cho thực thể User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id); // Đặt Id làm khóa chính
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd(); // Thiết lập tự tăng

                entity.HasIndex(e => e.Username)
                      .IsUnique(); // Đảm bảo Username là duy nhất

                entity.Property(e => e.Email)
                      .IsRequired()
                      .HasMaxLength(100); // Đặt điều kiện cho cột Email

                // Thiết lập quan hệ giữa User và UserProfile (1-1)
                entity.HasOne(e => e.UserProfile)
                      .WithOne()
                      .HasForeignKey<UserProfile>(e => e.UserId); // UserProfile sẽ có khóa ngoại UserId
            });

            // Cấu hình cho thực thể Role
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id); // Đặt Id làm khóa chính
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd(); // Thiết lập tự tăng

                entity.HasIndex(e => e.Name)
                      .IsUnique(); // Đảm bảo tên Role là duy nhất
            });

            // Cấu hình cho thực thể UserProfile
            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.Id); // Đặt Id làm khóa chính
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd(); // Thiết lập tự tăng

                entity.Property(e => e.FullName)
                      .IsRequired()
                      .HasMaxLength(200); // Đặt điều kiện cho cột FullName

                entity.Property(e => e.Address)
                      .HasMaxLength(300); // Đặt độ dài tối đa cho cột Address
            });

            // Có thể thêm các cấu hình khác nếu cần
        }
    }
}
