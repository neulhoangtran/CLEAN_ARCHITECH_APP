using App.Domain.Entities;

namespace App.Domain.Repositories
{
    public interface IUserRepository
    {
        User GetById(int userId);
        User GetByUsername(string username);
        void Add(User user);
        void Update(User user);
        void Delete(int userId);
    }
}
