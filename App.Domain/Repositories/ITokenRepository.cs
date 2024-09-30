using App.Domain.Entities;
using System.Threading.Tasks;

namespace App.Domain.Repositories
{
    public interface ITokenRepository
    {
        Task<Token> GetByTokenAsync(string token);
        Task<List<Token>> GetTokensByUserIdAsync(int userId);
        void RemoveRange(IEnumerable<Token> tokens);
        Task AddTokenAsync(Token token);
        Task RemoveTokenAsync(Token token);
        Task<int> GetUserTokenCountAsync(int userId);
        void Update(Token token);
        Task SaveChangesAsync();
    }
}
