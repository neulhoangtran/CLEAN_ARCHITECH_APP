using App.Domain.Entities;
using App.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Infrastructure.Persistence
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly ApplicationDbContext _context;

        public PermissionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PermissionCategory>> GetAllCategoriesWithPermissionsAsync()
        {
            return await _context.PermissionCategories
                .Include(pc => pc.Permissions) // Bao gồm tất cả các Permissions của PermissionCategory
                .OrderBy(pc => pc.Order)
                .ToListAsync();
        }

        public async Task<IEnumerable<Permission>> GetAllAsync()
        {
            return await _context.Permissions.ToListAsync();
        }

        public async Task<Permission> GetByIdAsync(int id)
        {
            return await _context.Permissions.FindAsync(id);
        }

        public async Task<Permission> GetByNameAsync(string permissionName)
        {
            return await _context.Permissions.FirstOrDefaultAsync(p => p.Name == permissionName);
        }

        public async Task AddAsync(Permission permission)
        {
            await _context.Permissions.AddAsync(permission);
        }

        public void Update(Permission permission)
        {
            _context.Permissions.Update(permission);
        }

        public void Delete(Permission permission)
        {
            _context.Permissions.Remove(permission);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
