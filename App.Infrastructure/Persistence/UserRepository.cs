using App.Domain;
using App.Domain.Entities;
using App.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace App.Infrastructure.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public void Add(User user)
        {
            _context.Users.Add(user);  // Không cần await ở đây
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
        }


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public User GetByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }
        public IEnumerable<User> GetAll()
        {
            return _context.Users.Include(u => u.UserProfile).ToList();
        }
        public async Task<Paginate<User>> GetPaginatedUsersAsync(int pageIndex, int pageSize)
        {
            var totalItems = await _context.Users.CountAsync();
            var users = await _context.Users
                .Include(u => u.UserProfile)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new Paginate<User>(users, totalItems, pageIndex, pageSize);
        }
    }
}
