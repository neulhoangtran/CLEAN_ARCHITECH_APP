using System.Threading.Tasks;
using App.Domain.Entities;
namespace App.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);
        User GetByUsername(string username);
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByEmailAsync(string email);
        void Add(User user);
        void Update(User user);
        void Delete(User user);
        Task SaveChangesAsync();
        IEnumerable<User> GetAll();
        Task<Paginate<User>> GetPaginatedUsersAsync(int pageIndex, int pageSize, string sortBy = null, string filter = null);
        Task<List<User>> GetUsersByRoleAsync(int roleId);
    }
}
