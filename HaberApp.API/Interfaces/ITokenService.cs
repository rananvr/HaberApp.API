using HaberApp.API.Models;

namespace HaberApp.API.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}