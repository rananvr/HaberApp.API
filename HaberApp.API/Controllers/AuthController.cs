using HaberApp.API.Data;
using HaberApp.API.DTOs;
using HaberApp.API.Interfaces;
using HaberApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HaberApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ITokenService _tokenService;

        public AuthController(AppDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        // POST: api/Auth/register -> Kayıt Olma Kapısı
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
        {
            // 1. Email daha önce kullanılmış mı diye kilerde (veritabanında) kontrol ediyoruz
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                return BadRequest("Bu email adresi zaten kullanılıyor.");

            // 2. Yeni kullanıcı oluşturuyoruz ve şifresini BCrypt ile şifreliyoruz!
            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password), // Şifreleme burada yapılıyor
                Role = "User" // Varsayılan rol
            };

            // 3. Veritabanına kaydet
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Kayıt başarılı! Giriş yapabilirsiniz." });
        }

        // POST: api/Auth/login -> Giriş Yapma Kapısı
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            // 1. Kullanıcıyı emaili üzerinden bul
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

            // 2. Kullanıcı yoksa VEYA girdiği şifrenin şifrelenmiş hali veritabanındakiyle uyuşmuyorsa
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return Unauthorized("Email veya şifre hatalı!");

            // 3. Her şey doğruysa TokenService'i çağır ve Token (Bileklik) üret
            var token = _tokenService.CreateToken(user);

            // 4. Token'ı Flutter'a gönder
            return Ok(new { Token = token, Message = "Giriş başarılı!" });
        }
    }
}