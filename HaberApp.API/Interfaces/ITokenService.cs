using HaberApp.API.Models;

namespace HaberApp.API.Interfaces
{
    public interface ITokenService
    {
        // Kullanıcı giriş yaptığında ona özel bir Token (string) üretecek metot
        string CreateToken(User user);
    }
}