using App.Application.DTOs;
using App.Domain;

namespace App.Application.Interfaces
{
    public interface IUserService
    {
        Task<Paginate<UserDto>> GetPaginatedUsersAsync(int pageIndex, int pageSize, string sortBy = null, string filter = null); // Thêm phương thức cho phân trang
        Task<UserDto> GetUserByIdAsync(int userId);
        Task UpdateUserAsync(int userId, UserDto userDto);
        Task DeleteUserAsync(int userId);
        //IEnumerable<UserDto> GetAllUsers();
    }
}
