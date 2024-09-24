using System.Threading.Tasks;
using App.Domain.Entities;

namespace App.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);
        User GetByUsername(string username);
        void Add(User user);
        void Update(User user);
        Task SaveChangesAsync(); 
    }
}
