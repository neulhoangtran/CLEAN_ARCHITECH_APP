using App.Domain.Entities;
using App.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Persistence
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public  IEnumerable<Role> GetAll() {
            return _context.Roles;
        }

        public async Task<Role> GetByIdAsync(int id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task<Role> GetByNameAsync(string name)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == name);
        }

        public void Add(Role role)
        {
            _context.Roles.Add(role);
        }

        public void Update(Role role)
        {
            _context.Roles.Update(role);
        }

        // Thực thi phương thức Delete theo interface
        public void Delete(Role role)
        {
            if (role != null)
            {
                _context.Roles.Remove(role);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
