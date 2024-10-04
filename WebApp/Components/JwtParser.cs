using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace WebApp.Components
{
    public static class JwtParser
    {
        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);

            return token.Claims;
        }
    }
}
