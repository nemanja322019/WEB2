using WebApplication.DTO;
using WebApplication.DTO.UserDTO;

namespace WebApplication.Interfaces
{
    public interface IUserService
    {
        public void RegisterUser(RegisterDTO registerDTO);
        public string Login(LoginDTO loginDTO);
        public DisplayProfileDTO FindById(int id);
        public DisplayProfileDTO UpdateProfile(int id, UpdateProfileDTO updateProfileDTO);
        public IEnumerable<DisplayProfileDTO> GetSellers();
        public DisplayProfileDTO VerifySeller(int id, bool isAccepted);
        public DisplayProfileDTO ChangePassword(int id,ChangePasswordDTO changePasswordDTO);
    }
}
