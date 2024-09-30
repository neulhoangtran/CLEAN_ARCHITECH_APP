using System.Threading.Tasks;
using App.Application.Interfaces;
using App.Domain.Common;
using App.Domain.Repositories;

namespace App.Application.Services
{
    public class PaginateService<T> : IPaginateService<T> where T : class
    {
        private readonly IPaginateRepository<T> _repository;

        public PaginateService(IPaginateRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<Paginate<T>> GetPaginatedListAsync(int pageIndex, int pageSize)
        {
            return await _repository.GetPaginatedListAsync(pageIndex, pageSize);
        }
    }
}
