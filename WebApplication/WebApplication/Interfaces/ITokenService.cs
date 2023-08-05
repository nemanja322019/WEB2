using WebApplication.Enums;

namespace WebApplication.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(int id, string username, UserTypes userType);
    }
}
