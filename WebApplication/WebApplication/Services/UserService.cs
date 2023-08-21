using AutoMapper;
using AutoMapper.Internal;
using System.Text.RegularExpressions;
using WebApplication.DTO;
using WebApplication.DTO.UserDTO;
using WebApplication.Enums;
using WebApplication.Infrastructure;
using WebApplication.Interfaces;
using WebApplication.Models;

namespace WebApplication.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly WebApplicationDbContext _dbContext;
        private readonly ITokenService _tokenService;

        public UserService(IMapper mapper, WebApplicationDbContext dbContext, ITokenService tokenService)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _tokenService = tokenService;
        }

        public IEnumerable<DisplayProfileDTO> GetSellers()
        {
            IEnumerable<User> temp = _dbContext.Users.Where(user => user.UserType == UserTypes.SELLER);

            IEnumerable<DisplayProfileDTO> allSellers = _mapper.Map<IEnumerable<DisplayProfileDTO>>(temp);

            return allSellers;
        }

        public DisplayProfileDTO VerifySeller(int id, bool isAccepted)
        {
            User user = _dbContext.Users.Find(id);
            if(user == null)
            {
                throw new Exception("Id not found!");
            }
            user.IsVerified = isAccepted;
            user.VerificationStatus = VerificationStatus.REJECTED;
            if(user.IsVerified)
            {
                user.VerificationStatus = VerificationStatus.ACCEPTED;
            }

            _dbContext.SaveChanges();
            return _mapper.Map<DisplayProfileDTO>(user);
        }

        public DisplayProfileDTO UpdateProfile(int id, UpdateProfileDTO updateProfileDTO)
        {
            User user = _dbContext.Users.Find(id);
            if (user == null)
            {
                throw new Exception("Id not found!");
            }

            string message;
            if(!ValidateFields(updateProfileDTO,out message))
            {
                throw new Exception(message);
            }

            user.Name = updateProfileDTO.Name;
            user.LastName = updateProfileDTO.LastName;
            user.Address = updateProfileDTO.Address;
            user.BirthDate = updateProfileDTO.BirthDate;
            _dbContext.SaveChanges();

            return _mapper.Map<DisplayProfileDTO>(user);

        }

        public DisplayProfileDTO ChangePassword(int id, ChangePasswordDTO changePasswordDTO)
        {
            User user = _dbContext.Users.Find(id);
            if (user == null)
            {
                throw new Exception("Id not found!");
            }

            if (!BCrypt.Net.BCrypt.Verify(changePasswordDTO.OldPassword, user.Password))
            {
                throw new Exception("Incorrect password");
            }

            if (String.IsNullOrEmpty(changePasswordDTO.NewPassword))
            {
                throw new Exception("Password can't be empty!");
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(changePasswordDTO.NewPassword, BCrypt.Net.BCrypt.GenerateSalt());
            _dbContext.SaveChanges();
            
            return _mapper.Map<DisplayProfileDTO>(user);
        }

        public string Login(LoginDTO loginDTO)
        {
            if (String.IsNullOrEmpty(loginDTO.Email))
            {
                throw new Exception("Email can't be empty!");
            }
            Regex emailRegex = new Regex(@"^([\w.-]+)@([\w-]+)((.(\w){2,3})+)$");

            if (!emailRegex.Match(loginDTO.Email).Success)
            {
                throw new Exception("Invalid email!");
            }
            if (!EmailExists(loginDTO.Email))
            {
                throw new Exception("Unknown email!");
            }
            if(String.IsNullOrEmpty(loginDTO.Password))
            {
                throw new Exception("Password can't be empty!");
            }

            User user = FindByEmail(loginDTO.Email);

            if (!BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password))
            {
                throw new Exception("Incorrect password");
            }

            return _tokenService.CreateToken(user.Id, user.Username, user.UserType);

        }

        public void RegisterUser(RegisterDTO registerDTO)
        {
            string message;
            if (!ValidateFields(registerDTO, out message))
            {
                throw new Exception(message);
            }

            User user = _mapper.Map<User>(registerDTO);
            user.VerificationStatus = VerificationStatus.PENDING;
            if(user.UserType != UserTypes.SELLER)
            {
                user.IsVerified = true;
                user.VerificationStatus = VerificationStatus.ACCEPTED;
            }
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, BCrypt.Net.BCrypt.GenerateSalt());
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            
            
        }

        public bool ValidateFields(UpdateProfileDTO updateProfileDTO, out string message)
        {
            message = "";
            if (String.IsNullOrWhiteSpace(updateProfileDTO.Name))
            {
                message = "Name can't be empty!";
                return false;
            }

            if (String.IsNullOrWhiteSpace(updateProfileDTO.LastName))
            {
                message = "Last name can't be empty!";
                return false;
            }

            if (String.IsNullOrWhiteSpace(updateProfileDTO.Address))
            {
                message = "Address can't be empty!";
                return false;
            }

            if(updateProfileDTO.BirthDate.Year < 1900 || updateProfileDTO.BirthDate > DateTime.Now)
            {
                message = "Invalid birth date!";
                return false;
            }
            return true;
        }

        public bool ValidateFields(RegisterDTO registerDTO, out string message)
        {
            message = "";

            if(!String.Equals(registerDTO.UserType.ToLower(),"customer") && !String.Equals(registerDTO.UserType.ToLower(), "seller") && !String.Equals(registerDTO.UserType.ToLower(), "admin"))
            {
                message = "Invalid user type!";
                return false;
            }

            Regex emailRegex = new Regex(@"^([\w.-]+)@([\w-]+)((.(\w){2,3})+)$");

            if (!emailRegex.Match(registerDTO.Email).Success)
            {
                message = "Invalid email!";
                return false;
            }

            if (String.IsNullOrWhiteSpace(registerDTO.Username))
            {
                message = "Username can't be empty!";
                return false;
            }

            if (String.IsNullOrWhiteSpace(registerDTO.Password))
            {
                message = "Password can't be empty!";
                return false;
            }

            if (String.IsNullOrWhiteSpace(registerDTO.Name))
            {
                message = "Name can't be empty!";
                return false;
            }

            if (String.IsNullOrWhiteSpace(registerDTO.LastName))
            {
                message = "Last name can't be empty!";
                return false;
            }

            if (String.IsNullOrWhiteSpace(registerDTO.Address))
            {
                message = "Address can't be empty!";
                return false;
            }

            if (registerDTO.BirthDate.Year < 1900 || registerDTO.BirthDate > DateTime.Now)
            {
                message = "Invalid birth date!";
                return false;
            }

            if (UserExists(registerDTO.Username))
            {
                message = "User already exists!";
                return false;
            }

            if (EmailExists(registerDTO.Email))
            {
                message = "Email already in use!";
                return false;
            }

            return true;
        }

        public DisplayProfileDTO FindById(int id)
        {
            User user = _dbContext.Users.Find(id);
            if (user == null)
            {
                throw new Exception("Id not found!");
            }
            return _mapper.Map<DisplayProfileDTO>(user);
        }

        public User FindByUsername(string username)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Username == username);
            return user;
        }

        public User FindByEmail(string email)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == email);
            return user;
        }

        public bool UserExists(string username)
        {
            return _dbContext.Users.Any(a => a.Username == username);
        }

        public bool EmailExists(string email)
        {
            return _dbContext.Users.Any(a => a.Email == email);
        }

    }
}
