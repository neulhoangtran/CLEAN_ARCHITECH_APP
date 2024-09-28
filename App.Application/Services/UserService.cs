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

        // Thêm phương thức GetUserById
        public async Task<UserDto> GetUserById(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return null;

            return new UserDto
            {
                ID = user.ID,
                Username = user.Username,
                EmployeeId = user.EmployeeId,
                Email = user.Email,
                Status = user.Status
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

            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            var userUpdatedEvent = new UserUpdatedEvent(user.ID, user.Email);
            _eventBus.Publish(userUpdatedEvent);
        }

        public async Task DeleteUser(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            user.Deactivate();
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            var userDeletedEvent = new UserDeletedEvent(user.ID);
            _eventBus.Publish(userDeletedEvent);
        }
    }
}
