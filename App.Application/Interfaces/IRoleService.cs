using App.Application.DTOs;
using App.Domain;
using System.Collections.Generic;

namespace App.Application.Interfaces
{
    public interface IRoleService
    {
        // Lấy danh sách tất cả các vai trò
        IEnumerable<RoleDto> GetAllRoles();

        Task<Paginate<RoleDto>> GetPaginatedRolesAsync(int pageIndex, int pageSize);
        // Lấy thông tin vai trò theo ID
        Task<RoleDto> GetRoleById(int roleId);

        // Tạo mới một vai trò
        Task CreateRoleAsync(string roleName);

        // Cập nhật thông tin một vai trò
        Task UpdateRoleAsync(int roleId, string roleName);

        // Xóa một vai trò
        Task DeleteRoleAsync(int roleId);

        Task AddUserToRoleAsync(int roleId, int userId);

    }
}
