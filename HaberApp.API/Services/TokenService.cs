using HaberApp.API.Interfaces;
using HaberApp.API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HaberApp.API.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        // Constructor: appsettings.json içindeki o gizli anahtarı buraya çekiyoruz
        public TokenService(IConfiguration config)
        {
            _config = config;
            // Anahtarımızı dijital bir şifreleme formatına çeviriyoruz
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        }

        public string CreateToken(User user)
        {
            // 1. Bilekliğin (Token) içine yazılacak bilgiler (Claims)
            // Kullanıcının kim olduğunu Token'a bakarak anlayacağız
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), // Kullanıcı ID'si
                new Claim(JwtRegisteredClaimNames.Email, user.Email),       // Kullanıcı Email'i
                new Claim(ClaimTypes.Role, user.Role)                       // Kullanıcı Rolü (Admin vs.)
            };

            // 2. Şifreleme algoritmasını seçiyoruz
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            // 3. Token'ın genel ayarları (Kim üretti, kime üretildi, ne zaman süresi bitecek?)
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7), // Token 7 gün geçerli olsun
                SigningCredentials = creds,
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"]
            };

            // 4. Token'ı oluştur ve geriye metin (string) olarak döndür
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}