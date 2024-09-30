using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Common;

namespace App.Domain.Repositories
{
    public interface IPaginateRepository<T> where T : class
    {
        Task<Paginate<T>> GetPaginatedListAsync(int pageIndex, int pageSize);
    }
}
