using App.Domain.Entities;
using System.Collections.Generic;

namespace App.Domain.Repositories
{
    public interface IRoleRepository
    {
        // Lấy tất cả các vai trò
        IEnumerable<Role> GetAll();

        // Lấy thông tin vai trò theo ID
        Role GetById(int id);

        // Lấy thông tin vai trò theo tên vai trò
        Role GetByName(string roleName);

        // Thêm mới một vai trò
        void Add(Role role);

        // Cập nhật thông tin một vai trò
        void Update(Role role);

        // Xóa một vai trò
        void Delete(Role role);

        // Lưu thay đổi vào cơ sở dữ liệu
        void SaveChanges();
    }
}
