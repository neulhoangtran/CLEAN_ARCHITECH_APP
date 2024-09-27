using App.Domain.Entities;
using System.Threading.Tasks;

namespace App.Domain.Repositories
{
    public interface ITokenRepository
    {
        Task AddAsync(Token token); // Thêm token vào cơ sở dữ liệu
        Task<Token> GetByTokenAsync(string token); // Lấy token theo giá trị token
        void Update(Token token); // Cập nhật token
        Task SaveChangesAsync(); // Lưu các thay đổi vào cơ sở dữ liệu
    }
}
