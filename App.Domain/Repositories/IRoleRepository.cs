using App.Domain.Entities;

namespace App.Domain.Repositories
{
    public interface IRoleRepository
    {
        Role GetByName(string roleName);
        void Add(Role role);
        void Update(Role role);
        void Delete(string roleName);
    }
}
