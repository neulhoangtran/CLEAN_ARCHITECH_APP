using App.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Application.Interfaces
{
    public interface IPermissionService
    {
        Task<IEnumerable<PermissionDto>> GetAllPermissionsAsync();
        Task<IEnumerable<PermissionCategoryDto>> GetPermissionsGroupedByCategoryAsync();
        Task<PermissionDto> GetPermissionByIdAsync(int permissionId);
        Task CreatePermissionAsync(string permissionName, string description);
        Task UpdatePermissionAsync(int permissionId, string permissionName, string description);
        Task DeletePermissionAsync(int permissionId);
    }
}
