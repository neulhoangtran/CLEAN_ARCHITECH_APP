using App.Application.DTOs;

namespace App.Application.Interfaces
{
    public interface IUserService
    {
        void RegisterUser(UserDto userDto);
        Task<UserDto> GetUserById(int userId);
        Task UpdateUser(int userId, UserDto userDto);
        Task DeleteUser(int userId);
        Task<UserDto> Login(string username, string password);
    }
}
