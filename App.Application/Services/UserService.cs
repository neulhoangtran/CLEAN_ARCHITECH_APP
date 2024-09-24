using App.Application.DTOs;
using App.Application.Interfaces;
using App.Domain.Entities;
using App.Domain.Events;
using App.Domain.Repositories;
using App.Domain.Services;
using App.Domain.Events;

namespace App.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserDomainService _userDomainService;
        private readonly IEventBus _eventBus; // Thêm EventBus

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

            var user = new User(userDto.Username, userDto.Email, userDto.PasswordHash, userDto.Role);
            _userRepository.Add(user);

            // Phát sự kiện UserRegisteredEvent
            var userRegisteredEvent = new UserRegisteredEvent(user.Id, user.Username, user.Email);
            _eventBus.Publish(userRegisteredEvent); // Publish sự kiện
        }

        public UserDto GetUserById(int userId)
        {
            var user = _userRepository.GetById(userId);
            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.IsActive
            };
        }

        public void UpdateUser(int userId, UserDto userDto)
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
                throw new Exception("User not found");

            user.UpdateUserProfile(new UserProfile(userDto.FullName, userDto.Address, userDto.PhoneNumber));
            user.Email = userDto.Email;
            user.Role = userDto.Role;

            _userRepository.Update(user);

            // Phát sự kiện UserUpdatedEvent
            var userUpdatedEvent = new UserUpdatedEvent(user.Id, user.Email);
            _eventBus.Publish(userUpdatedEvent); // Publish sự kiện
        }

        public void DeleteUser(int userId)
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
                throw new Exception("User not found");

            user.Deactivate(); // Chuyển trạng thái người dùng sang không hoạt động
            _userRepository.Update(user);

            // Phát sự kiện UserDeletedEvent
            var userDeletedEvent = new UserDeletedEvent(user.Id);
            _eventBus.Publish(userDeletedEvent); // Publish sự kiện
        }
    }
}
