using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Models;

namespace WebApplication1.JWT
{
    public class TokenManager
    {
        private JwtSecurityTokenHandler tokenHandler = new();
        private readonly byte[] _jwtSecretKey = Encoding.ASCII.GetBytes("your-secret-key");







        public string GenerateJWTToken(User userInfo)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                    {
                        new(ClaimTypes.Name, "username"),
                        new(ClaimTypes.Email, "user@example.com")
                        // 필요한 경우에 사용자 클레임 추가
                    }),
                Expires = DateTime.UtcNow.AddHours(1), // 토큰 만료 시간 설정
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_jwtSecretKey), SecurityAlgorithms.HmacSha256Signature)
            
            
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);




            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.Sha512);
            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
        new Claim(ClaimTypes.Role, userInfo.UserRole),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }




        public User AuthenticateUser(User loginCredentials)
        {
            User user = appUsers.SingleOrDefault(x => x.UserName == loginCredentials.UserName && x.Password == loginCredentials.Password);
            return user;
        }


    }


}
