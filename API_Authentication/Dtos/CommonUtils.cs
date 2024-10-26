using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace API_Authentication.Dtos
{
    public static class CommonUtils
    {
        public static string GetCurrentUserId(IHttpContextAccessor httpContextAccessor)
        {
            var claims = httpContextAccessor.HttpContext?.User?.Identity as ClaimsIdentity;
            //nếu trong program dùng JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            //thì các claim type sẽ không bị ghi đè tên nên phải dùng trực tiếp "sub"
            var claim = claims?.FindFirst(JwtRegisteredClaimNames.Name) ?? claims?.FindFirst("sub");
            if (claim == null)
            {
                throw new Exception($"Tài khoản không chứa claim \"{System.Security.Claims.ClaimTypes.NameIdentifier}\"");
            }
            return claim.Value;
        }
    }
}
