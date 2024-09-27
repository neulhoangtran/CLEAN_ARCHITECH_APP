using App.Application.DTOs;
using App.Application.Interfaces;
using App.Domain.Entities;
using App.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace App.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        // Constructor, inject repository
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public IEnumerable<RoleDto> GetAllRoles()
        {
            var roles = _roleRepository.GetAll();
            return roles.Select(r => new RoleDto
            {
                ID = r.ID,
                Name = r.RoleName
            }).ToList();
        }

        public RoleDto GetRoleById(int roleId)
        {
            var role = _roleRepository.GetById(roleId);
            if (role == null) return null;

            return new RoleDto
            {
                ID = role.ID,
                Name = role.RoleName
            };
        }

        public void CreateRole(string roleName)
        {
            if (_roleRepository.GetByName(roleName) != null)
                throw new Exception("Role already exists");

            var role = new Role
            {
                RoleName = roleName
            };
            _roleRepository.Add(role);
            //_roleRepository.SaveChanges();
        }

        public void UpdateRole(int roleId, string roleName)
        {
            var role = _roleRepository.GetById(roleId);
            if (role == null)
                throw new Exception("Role not found");

            role.RoleName = roleName;
            _roleRepository.Update(role);
            //_roleRepository.SaveChanges();
        }

        public void DeleteRole(int roleId)
        {
            var role = _roleRepository.GetById(roleId);
            if (role == null)
                throw new Exception("Role not found");

            _roleRepository.Delete(role);
            //_roleRepository.SaveChanges();
        }
    }
}
