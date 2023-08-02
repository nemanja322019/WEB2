using WebApplication.DTO;

namespace WebApplication.Interfaces
{
    public interface IUserService
    {
        //public bool UserExists(string username);
        //public LoginDTO SearchUser(string username);
        public RegisterDTO RegisterUser(RegisterDTO registerDTO);
    }
}
