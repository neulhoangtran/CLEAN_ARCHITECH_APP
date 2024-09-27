using App.Domain.Entities;
using App.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace App.Infrastructure.Persistence
{
    public class TokenRepository : ITokenRepository
    {
        private readonly ApplicationDbContext _context;

        public TokenRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Thêm token vào cơ sở dữ liệu
        public async Task AddAsync(Token token)
        {
            await _context.Tokens.AddAsync(token);
        }

        // Lấy token theo giá trị token
        public async Task<Token> GetByTokenAsync(string token)
        {
            return await _context.Tokens.SingleOrDefaultAsync(t => t.TokenValue == token);
        }

        // Cập nhật token
        public void Update(Token token)
        {
            _context.Tokens.Update(token);
        }

        // Lưu các thay đổi vào cơ sở dữ liệu
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
