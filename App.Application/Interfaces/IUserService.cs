using App.Application.DTOs;

namespace App.Application.Interfaces
{
    public interface IUserService
    {
        Task<PaginatedList<UserDto>> GetPaginatedUsersAsync(int pageIndex, int pageSize); // Thêm phương thức cho phân trang
        Task<UserDto> GetUserById(int userId);
        Task UpdateUser(int userId, UserDto userDto);
        Task DeleteUser(int userId);
        IEnumerable<UserDto> GetAllUsers();
    }
}
