using App.Application.DTOs;

namespace App.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetUserById(int userId);
        Task UpdateUser(int userId, UserDto userDto);
        Task DeleteUser(int userId);
    }
}
