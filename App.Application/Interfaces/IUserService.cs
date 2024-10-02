using App.Application.DTOs;
using App.Domain;

namespace App.Application.Interfaces
{
    public interface IUserService
    {
        Task<Paginate<UserDto>> GetPaginatedUsersAsync(int pageIndex, int pageSize); // Thêm phương thức cho phân trang
        Task<UserDto> GetUserByIdAsync(int userId);
        Task UpdateUserAsync(int userId, UserDto userDto);
        Task DeleteUserAsync(int userId);
        //IEnumerable<UserDto> GetAllUsers();
    }
}
