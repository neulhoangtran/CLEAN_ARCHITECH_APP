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
        //public async Task<Paginate<User>> GetPaginatedUsersAsync(int pageIndex, int pageSize)
        //{
        //    var totalItems = await _context.Users.CountAsync();
        //    var users = await _context.Users
        //        .Include(u => u.UserProfile)
        //        .Skip((pageIndex - 1) * pageSize)
        //        .Take(pageSize)
        //        .ToListAsync();

        //    return new Paginate<User>(users, totalItems, pageIndex, pageSize);
        //}
        public async Task<Paginate<User>> GetPaginatedUsersAsync(int pageIndex, int pageSize, string sortBy = null, string filter = null)
        {
            var query = _context.Users.Include(u => u.UserProfile) // Nạp thông tin UserProfile
                                .Include(u => u.UserRole) // Nạp thông tin các UserRole
                                .AsQueryable();

            // Nếu có tham số lọc, thêm điều kiện lọc
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(u => u.Username.Contains(filter) || u.Email.Contains(filter));
            }

            // Sắp xếp theo thuộc tính được chỉ định
            switch (sortBy)
            {
                case "username_asc":
                    query = query.OrderBy(u => u.Username);
                    break;
                case "username_desc":
                    query = query.OrderByDescending(u => u.Username);
                    break;
                case "email_asc":
                    query = query.OrderBy(u => u.Email);
                    break;
                case "email_desc":
                    query = query.OrderByDescending(u => u.Email);
                    break;
                default:
                    query = query.OrderBy(u => u.ID); // Sắp xếp mặc định theo ID
                    break;
            }

            // Lấy tổng số bản ghi trước khi phân trang
            var totalItems = await query.CountAsync();

            // Phân trang
            var users = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new Paginate<User>(users, totalItems, pageIndex, pageSize);
        }

        public async Task<List<User>> GetUsersByRoleAsync(int roleId)
        {
            // Sử dụng linq để lấy danh sách người dùng có roleId tương ứng
            return await _context.Users
                                 .Where(u => u.UserRole != null && u.UserRole.RoleID == roleId) // Kiểm tra trực tiếp thuộc tính UserRole
                                 .Include(u => u.UserProfile) // Gộp cả thông tin UserProfile
                                 .ToListAsync();
        }
    }
}
