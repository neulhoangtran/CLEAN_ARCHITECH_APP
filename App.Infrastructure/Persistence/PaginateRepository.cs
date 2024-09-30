using System.Linq;
using System.Threading.Tasks;
using App.Domain.Common;
using App.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Persistence
{
    public class PaginateRepository<T> : IPaginateRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        public PaginateRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Paginate<T>> GetPaginatedListAsync(int pageIndex, int pageSize)
        {
            var source = _context.Set<T>().AsQueryable();

            var totalItems = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new Paginate<T>(items, totalItems, pageIndex, pageSize);
        }
    }
}
