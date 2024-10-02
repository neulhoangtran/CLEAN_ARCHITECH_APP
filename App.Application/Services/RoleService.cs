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

        public async Task<RoleDto> GetRoleById(int roleId)
        {
            var role = await _roleRepository.GetByIdAsync(roleId);
            if (role == null) return null;

            return new RoleDto
            {
                ID = role.ID,
                Name = role.RoleName
            };
        }

        public async Task CreateRoleAsync(string roleName)
        {
            if (await _roleRepository.GetByNameAsync(roleName) != null)
                throw new Exception("Role already exists");

            var role = new Role
            {
                RoleName = roleName
            };
            _roleRepository.Add(role);
            await _roleRepository.SaveChangesAsync();
        }

        public async Task UpdateRoleAsync(int roleId, string roleName)
        {
            var role = await _roleRepository.GetByIdAsync(roleId);
            if (role == null)
                throw new Exception("Role not found");

            role.RoleName = roleName;
            _roleRepository.Update(role);
            await _roleRepository.SaveChangesAsync(); 
        }

        public async Task DeleteRoleAsync(int roleId)
        {
            var role =  await _roleRepository.GetByIdAsync(roleId);
            if (role == null)
                throw new Exception("Role not found");

             _roleRepository.Delete(role);
            await _roleRepository.SaveChangesAsync();
        }
    }
}
