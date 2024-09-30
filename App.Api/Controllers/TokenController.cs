using Microsoft.AspNetCore.Mvc;
using App.Application.Interfaces;
using App.Api.Models;
using System.Threading.Tasks;

namespace App.Api.Controllers
{
    [Route("api/token")]  // Đường dẫn API chữ thường
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        // Inject AuthService vào Controller
        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        // API để làm mới Access Token
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var newTokens = await _tokenService.RefreshAccessTokenAsync(request.RefreshToken);
            if (newTokens == null)
            {
                return Unauthorized(new { Message = "Invalid or expired refresh token" });
            }

            return Ok(newTokens);
        }

    }
}
