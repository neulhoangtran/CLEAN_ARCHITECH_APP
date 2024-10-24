using App.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Domain.Repositories
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<Permission>> GetAllAsync();
        Task<IEnumerable<PermissionCategory>> GetAllCategoriesWithPermissionsAsync();
        Task<Permission> GetByIdAsync(int id);
        Task<Permission> GetByNameAsync(string permissionName);
        Task AddAsync(Permission permission);
        void Update(Permission permission);
        void Delete(Permission permission);
        Task SaveChangesAsync();
    }
}
