using App.Application.DTOs;
using App.Application.Interfaces;
using App.Domain;
using App.Domain.Entities;
using App.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace App.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;

        // Constructor, inject repository
        public RoleService(IRoleRepository roleRepository, IUserRepository userRepository)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        // Thêm phương thức lấy danh sách role phân trang
        public async Task<Paginate<RoleDto>> GetPaginatedRolesAsync(int pageIndex, int pageSize)
        {
            // Lấy dữ liệu từ repository với phân trang
            var paginatedRoles = await _roleRepository.GetPaginatedRolesAsync(pageIndex, pageSize);

            // Chuyển đổi dữ liệu sang DTO
            var roleDtos = paginatedRoles.Items.Select(r => new RoleDto
            {
                ID = r.ID,
                Name = r.RoleName
            }).ToList();

            // Trả về dữ liệu phân trang với các role đã chuyển đổi
            return new Paginate<RoleDto>(roleDtos, paginatedRoles.TotalItems, pageIndex, pageSize);
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


        public async Task AddUserToRoleAsync(int roleId, int userId)
        {
            var role = await _roleRepository.GetByIdAsync(roleId);
            if (role == null)
            {
                throw new Exception("Role not found");
            }

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            // Kiểm tra xem người dùng đã thuộc role chưa
            if (!role.UserRoles.Any(ur => ur.UserID == userId))
            {
                role.UserRoles.Add(new UserRole { RoleID = roleId, UserID = userId });
                _roleRepository.Update(role);
                await _roleRepository.SaveChangesAsync();
            }
        }
                    
    }
}
