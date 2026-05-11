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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                return BadRequest("Bu email adresi zaten kullanılıyor.");

            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password), // Şifreleme
                Role = "User" // varsayılan
            };

            //  Veritabanı
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Kayıt başarılı! Giriş yapabilirsiniz." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return Unauthorized("Email veya şifre hatalı!");

            var token = _tokenService.CreateToken(user);

            return Ok(new { Token = token, Message = "Giriş başarılı!" });
        }
    }
}