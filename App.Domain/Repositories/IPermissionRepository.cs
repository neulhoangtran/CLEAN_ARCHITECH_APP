using App.Domain.Entities;
using System.Collections.Generic;

namespace App.Domain.Repositories
{
    public interface IPermissionRepository
    {
        IEnumerable<Permission> GetAll();
        Permission GetById(int id);
        Permission GetByName(string permissionName);
        void Add(Permission permission);
        void Update(Permission permission);
        void Delete(Permission permission);
        void SaveChanges();
    }
}
