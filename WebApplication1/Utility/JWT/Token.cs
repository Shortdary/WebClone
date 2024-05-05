using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Models;

namespace WebApplication1.Utility.JWT
{
    /// <summary>
    /// JWT 토큰을 관리하는 class
    /// JWT 생성과 검증을 담당
    /// </summary>
    public class TokenManager
    {

        private readonly IConfiguration? _config;
        private readonly JwtSecurityTokenHandler tokenHandler = new();

        public TokenManager(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateJWTToken(User userInfo)
        {
            SymmetricSecurityKey _jwtSecretKey = new(Encoding.UTF8.GetBytes(_config.GetValue<string>("Jwt:SecretKey")));
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new Claim[]
                        {
                            new(ClaimTypes.NameIdentifier, userInfo.UserId.ToString()),
                            new(ClaimTypes.Name, userInfo.Nickname)
                        }),

                Issuer = _config.GetValue<string>("Jwt:Issuer"),
                Audience = _config.GetValue<string>("Jwt:Audience"),
                Expires = DateTime.UtcNow.AddMinutes(30), // 토큰 만료 시간 설정
                SigningCredentials = new(_jwtSecretKey, SecurityAlgorithms.HmacSha256Signature)
            };
            foreach (string role in userInfo.Roles.Split(","))
            {
                tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        public string VerifyJWTToken(string token)
        {
            return token;
        }
    }
}
