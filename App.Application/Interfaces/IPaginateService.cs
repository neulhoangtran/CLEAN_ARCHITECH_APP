using System.Threading.Tasks;
using App.Domain.Common;

namespace App.Application.Interfaces
{
    public interface IPaginateService<T> where T : class
    {
        Task<Paginate<T>> GetPaginatedListAsync(int pageIndex, int pageSize);
    }
}
