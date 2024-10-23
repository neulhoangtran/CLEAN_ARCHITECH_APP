using App.Application.DTOs;
using App.Application.Interfaces;
using App.Domain.Entities;
using App.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace App.Application.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public IEnumerable<PermissionDto> GetAllPermissions()
        {
            var permissions = _permissionRepository.GetAll();
            return permissions.Select(p => new PermissionDto
            {
                ID = p.ID,
                Name = p.Name,
                Description = p.Description
            }).ToList();
        }

        public PermissionDto GetPermissionById(int permissionId)
        {
            var permission = _permissionRepository.GetById(permissionId);
            if (permission == null) return null;

            return new PermissionDto
            {
                ID = permission.ID,
                Name = permission.Name,
                Description = permission.Description
            };
        }

        public void CreatePermission(string permissionName, string description)
        {
            if (_permissionRepository.GetByName(permissionName) != null)
                throw new Exception("Permission already exists");

            var permission = new Permission
            {
                Name = permissionName,
                Description = description
            };
            _permissionRepository.Add(permission);
            _permissionRepository.SaveChanges();
        }

        public void UpdatePermission(int permissionId, string permissionName, string description)
        {
            var permission = _permissionRepository.GetById(permissionId);
            if (permission == null)
                throw new Exception("Permission not found");

            permission.Name = permissionName;
            permission.Description = description;
            _permissionRepository.Update(permission);
            _permissionRepository.SaveChanges();
        }

        public void DeletePermission(int permissionId)
        {
            var permission = _permissionRepository.GetById(permissionId);
            if (permission == null)
                throw new Exception("Permission not found");

            _permissionRepository.Delete(permission);
            _permissionRepository.SaveChanges();
        }
    }
}
