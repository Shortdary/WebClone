using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Models;

namespace WebApplication1.JWT
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
                            new(ClaimTypes.Name, userInfo.Nickname),
                            // 필요한 경우에 사용자 클레임 추가
                        }),

                Issuer = _config.GetValue<string>("Jwt:Issuer"),
                Audience = _config.GetValue<string>("Jwt:Audience"),
                Expires = DateTime.UtcNow.AddMinutes(5), // 토큰 만료 시간 설정
                SigningCredentials = new(_jwtSecretKey, SecurityAlgorithms.HmacSha256Signature)

            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(token);

            return tokenString;


            //        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            //        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.Sha512);
            //        var claims = new[]
            //        {
            //    new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
            //    new Claim(ClaimTypes.Role, userInfo.UserRole),
            //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //};

            //        var token = new JwtSecurityToken(
            //            issuer: _config["Jwt:Issuer"],
            //            audience: _config["Jwt:Audience"],
            //            claims: claims,
            //            expires: DateTime.Now.AddHours(1),
            //            signingCredentials: credentials
            //        );

            //        return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string VerifyJWTToken(string token)
        {
            return token;
        }
    }
}
