using App.Application.DTOs;
using App.Application.Interfaces;
using App.Domain.Entities;
using App.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Application.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<IEnumerable<PermissionCategoryDto>> GetPermissionsGroupedByCategoryAsync()
        {
            var permissions = await _permissionRepository.GetAllCategoriesWithPermissionsAsync();

            // Đảm bảo permissions không null
            if (permissions == null || !permissions.Any())
            {
                return new List<PermissionCategoryDto>();
            }

            var result = permissions.Select(c => new PermissionCategoryDto
            {
                CategoryID = c.ID,
                CategoryName = c.Name,
                CategoryDescription = c.Description,
                CategoryOrder = c.Order,
                Permissions = c.Permissions.Select(p => new PermissionDto
                {
                    ID = p.ID,
                    Name = p.Name,
                    Description = p.Description
                }).ToList()
            }).ToList();

            return result;
        }

        public async Task<IEnumerable<PermissionDto>> GetAllPermissionsAsync()
        {
            var permissions = await _permissionRepository.GetAllAsync();
            return permissions.Select(p => new PermissionDto
            {
                ID = p.ID,
                Name = p.Name,
                Description = p.Description
            }).ToList();
        }

        public async Task<PermissionDto> GetPermissionByIdAsync(int permissionId)
        {
            var permission = await _permissionRepository.GetByIdAsync(permissionId);
            if (permission == null) return null;

            return new PermissionDto
            {
                ID = permission.ID,
                Name = permission.Name,
                Description = permission.Description
            };
        }

        public async Task CreatePermissionAsync(string permissionName, string description)
        {
            var existingPermission = await _permissionRepository.GetByNameAsync(permissionName);
            if (existingPermission != null)
                throw new Exception("Permission already exists");

            var permission = new Permission
            {
                Name = permissionName,
                Description = description
            };
            await _permissionRepository.AddAsync(permission);
            await _permissionRepository.SaveChangesAsync();
        }

        public async Task UpdatePermissionAsync(int permissionId, string permissionName, string description)
        {
            var permission = await _permissionRepository.GetByIdAsync(permissionId);
            if (permission == null)
                throw new Exception("Permission not found");

            permission.Name = permissionName;
            permission.Description = description;
            _permissionRepository.Update(permission);
            await _permissionRepository.SaveChangesAsync();
        }

        public async Task DeletePermissionAsync(int permissionId)
        {
            var permission = await _permissionRepository.GetByIdAsync(permissionId);
            if (permission == null)
                throw new Exception("Permission not found");

            _permissionRepository.Delete(permission);
            await _permissionRepository.SaveChangesAsync();
        }
    }
}
