using App.Application.DTOs;
namespace App.Application.Interfaces
{
    public interface IUserService
    {
        void RegisterUser(UserDto userDto);
        UserDto GetUserById(int userId);
        void UpdateUser(int userId, UserDto userDto);
        void DeleteUser(int userId);
    }
}
