using WebApplication.DTO;

namespace WebApplication.Interfaces
{
    public interface IUserService
    {
        public RegisterDTO RegisterUser(RegisterDTO registerDTO);
        public string Login(LoginDTO loginDTO);
    }
}
