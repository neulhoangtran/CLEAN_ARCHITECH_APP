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

        public Role GetById(int id)
        {
            return _context.Roles.Find(id);
        }

        public Role GetByName(string name)
        {
            return _context.Roles.FirstOrDefault(r => r.Name == name);
        }

        public void Add(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
        }

        public void Update(Role role)
        {
            _context.Roles.Update(role);
            _context.SaveChanges();
        }

        // Thực thi phương thức Delete theo interface
        public void Delete(int id)
        {
            var role = _context.Roles.Find(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                _context.SaveChanges();
            }
        }

        // Thêm phương thức Delete với tham số string theo interface
        public void Delete(string roleName)
        {
            var role = _context.Roles.FirstOrDefault(r => r.Name == roleName);
            if (role != null)
            {
                _context.Roles.Remove(role);
                _context.SaveChanges();
            }
        }
    }
}
