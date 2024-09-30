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
        public async Task AddTokenAsync(Token token)
        {
            await _context.Tokens.AddAsync(token);
            await _context.SaveChangesAsync();
        }

        // Lấy token theo giá trị token
        public async Task<Token> GetByTokenAsync(string token)
        {
            return await _context.Tokens.SingleOrDefaultAsync(t => t.TokenValue == token);
        }

        // Xóa token
        public async Task RemoveTokenAsync(Token token)
        {
            _context.Tokens.Remove(token);
            await _context.SaveChangesAsync();
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

        public async Task<int> GetUserTokenCountAsync(int userId)
        {
            return await _context.Tokens.CountAsync(t => t.UserID == userId);
        }

        // Triển khai phương thức lấy các token theo userId
        public async Task<List<Token>> GetTokensByUserIdAsync(int userId)
        {
            return await _context.Tokens.Where(t => t.UserID == userId).ToListAsync();
        }

        public void RemoveRange(IEnumerable<Token> tokens)
        {
            _context.Tokens.RemoveRange(tokens);
        }
    }
}
