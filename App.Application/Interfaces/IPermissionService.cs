using App.Application.DTOs;
using System.Collections.Generic;

namespace App.Application.Interfaces
{
    public interface IPermissionService
    {
        // Lấy danh sách tất cả các quyền
        IEnumerable<PermissionDto> GetAllPermissions();

        // Lấy thông tin quyền theo ID
        PermissionDto GetPermissionById(int permissionId);

        // Tạo mới một quyền
        void CreatePermission(string permissionName, string description);

        // Cập nhật thông tin một quyền
        void UpdatePermission(int permissionId, string permissionName, string description);

        // Xóa một quyền
        void DeletePermission(int permissionId);
    }
}
