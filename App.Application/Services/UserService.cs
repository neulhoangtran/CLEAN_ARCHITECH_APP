using App.Application.DTOs;
using App.Application.Interfaces;
using App.Domain;
using App.Domain.Entities;
using App.Domain.Events;
using App.Domain.Events.User;
using App.Domain.Repositories;
using App.Domain.Services;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace App.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserDomainService _userDomainService;
        private readonly IEventBus _eventBus;

        public UserService(IUserRepository userRepository, UserDomainService userDomainService, IEventBus eventBus)
        {
            _userRepository = userRepository;
            _userDomainService = userDomainService;
            _eventBus = eventBus;
        }

        // Thêm phương thức GetUserById
        public async Task<UserDto> GetUserByIdAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return null;

            return new UserDto
            {
                ID = user.ID,
                Username = user.Username,
                EmployeeId = user.EmployeeId,
                Email = user.Email,
                Status = user.Status,
                FullName = user.UserProfile?.FullName,
                Address = user.UserProfile?.Address,
                PhoneNumber = user.UserProfile?.PhoneNumber
            };
        }

        // Thêm phương thức UpdateUser
        public async Task UpdateUserAsync(int userId, UserDto userDto)
        {
            // Lấy thông tin người dùng từ repository
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            // Kiểm tra và xử lý UserProfile (Cập nhật hoặc thêm mới)
            if (user.UserProfile != null)
            {
                // Nếu đã tồn tại UserProfile, chỉ cập nhật thông tin
                user.UserProfile.FullName = userDto.FullName ?? user.UserProfile.FullName;
                user.UserProfile.Address = userDto.Address ?? user.UserProfile.Address;
                user.UserProfile.PhoneNumber = userDto.PhoneNumber ?? user.UserProfile.PhoneNumber;
            }
            else
            {
                // Nếu UserProfile chưa tồn tại, thêm mới
                user.UserProfile = new UserProfile
                {
                    UserID = userId, // Ràng buộc với user hiện tại
                    FullName = userDto.FullName,
                    Address = userDto.Address,
                    PhoneNumber = userDto.PhoneNumber
                };
            }

            // Cập nhật thông tin cơ bản của người dùng
            user.Username = userDto.Username ?? user.Username;
            user.Email = userDto.Email ?? user.Email;

            // Cập nhật Role nếu có sự thay đổi
            if (userDto.Role > 0)
            {
                user.UserRoles.Clear(); // Xóa các role cũ
                user.UserRoles.Add(new UserRole { RoleID = userDto.Role, UserID = userId });
            }

            // Cập nhật thông tin người dùng trong repository
            _userRepository.Update(user);

            // Lưu các thay đổi
            await _userRepository.SaveChangesAsync();

            // Phát sự kiện cập nhật người dùng
            //var userUpdatedEvent = new UserUpdatedEvent(user.ID, user.Email);
            //_eventBus.Publish(userUpdatedEvent);
        }


        public async Task DeleteUserAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            _userRepository.Delete(user);  // Phương thức này sẽ được định nghĩa trong UserRepository
            await _userRepository.SaveChangesAsync();

            //var userDeletedEvent = new UserDeletedEvent(user.ID);
            //_eventBus.Publish(userDeletedEvent);
        }

        public async Task<Paginate<UserDto>> GetPaginatedUsersAsync(int pageIndex, int pageSize, string sortBy = null, string filter = null)
        {
            // Lấy danh sách người dùng phân trang từ repository với sắp xếp và lọc
            var paginatedUsers = await _userRepository.GetPaginatedUsersAsync(pageIndex, pageSize, sortBy, filter);

            var userDtos = paginatedUsers.Items.Select(u => new UserDto
            {
                ID = u.ID,
                Username = u.Username,
                Email = u.Email,
                PhoneNumber = u.UserProfile?.PhoneNumber,
                FullName = u.UserProfile?.FullName,
                Address = u.UserProfile?.Address
            }).ToList();

            return new Paginate<UserDto>(userDtos, paginatedUsers.TotalItems, pageIndex, pageSize);
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

        public async Task<List<UserDto>> GetUsersByRoleAsync(int roleId)
        {
            // Gọi repository để lấy danh sách người dùng theo roleId
            var users = await _userRepository.GetUsersByRoleAsync(roleId);

            // Chuyển đổi từ entity sang DTO
            return users.Select(user => new UserDto
            {
                ID = user.ID,
                Username = user.Username,
                Email = user.Email,
                FullName = user.UserProfile?.FullName
            }).ToList();
        }
    }
}
