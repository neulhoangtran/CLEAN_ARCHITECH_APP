using App.Application.DTOs;
using App.Application.Interfaces;
using App.Domain.Entities;
using App.Domain.Events;
using App.Domain.Events.User;
using App.Domain.Repositories;
using App.Domain.Services;
using System.Threading.Tasks;

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

        public void RegisterUser(UserDto userDto)
        {
            if (!_userDomainService.IsUsernameUnique(userDto.Username))
                throw new Exception("Username already exists");

            var user = new User(userDto.Username, userDto.EmployeeId, userDto.Email, userDto.PasswordHash, userDto.Role);
            _userRepository.Add(user);

            var userRegisteredEvent = new UserRegisteredEvent(user.Id, user.Username, user.Email);
            _eventBus.Publish(userRegisteredEvent);
        }

        // Thêm phương thức GetUserById
        public async Task<UserDto> GetUserById(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                EmployeeId = user.EmployeeId,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.IsActive
            };
        }

        // Thêm phương thức UpdateUser
        public async Task UpdateUser(int userId, UserDto userDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            user.UpdateUserProfile(new UserProfile(userDto.FullName, userDto.Address, userDto.PhoneNumber));
            user.Email = userDto.Email;
            user.Role = userDto.Role;

            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            var userUpdatedEvent = new UserUpdatedEvent(user.Id, user.Email);
            _eventBus.Publish(userUpdatedEvent);
        }

        // Thêm phương thức Login
        public async Task<UserDto> Login(string username, string password)
        {
            var user = _userRepository.GetByUsername(username);
            if (user == null || user.PasswordHash != password) // Giả sử so sánh trực tiếp, nên hash mật khẩu trước
            {
                return null;
            }

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role
            };
        }

        public async Task DeleteUser(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            user.Deactivate();
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            var userDeletedEvent = new UserDeletedEvent(user.Id);
            _eventBus.Publish(userDeletedEvent);
        }
    }
}
