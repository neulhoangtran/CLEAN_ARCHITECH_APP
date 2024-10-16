using App.Domain.Entities;
using System.Collections.Generic;

namespace App.Domain.Repositories
{
    public interface IRoleRepository
    {
        // Lấy tất cả các vai trò
        IEnumerable<Role> GetAll();

        Task<Paginate<Role>> GetPaginatedRolesAsync(int pageIndex, int pageSize);

        // Lấy thông tin vai trò theo ID
        Task<Role> GetByIdAsync(int id);

        // Lấy thông tin vai trò theo tên vai trò
        Task<Role> GetByNameAsync(string roleName);

        // Thêm mới một vai trò
        void Add(Role role);

        // Cập nhật thông tin một vai trò
        void Update(Role role);

        // Xóa một vai trò
        void Delete(Role role);

        // Lưu thay đổi vào cơ sở dữ liệu
        Task SaveChangesAsync();
    }
}
