using AutoMapper;
using AutoMapper.Internal;
using System.Text.RegularExpressions;
using WebApplication.DTO;
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

        public string Login(LoginDTO loginDTO)
        {
            if(!EmailExists(loginDTO.Email))
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
            }_tokenService.CreateToken(user.Id, user.Username, user.UserType);

            return _tokenService.CreateToken(user.Id, user.Username, user.UserType);

        }

        public RegisterDTO RegisterUser(RegisterDTO registerDTO)
        {
            string message;
            if (!ValidateFields(registerDTO, out message))
            {
                throw new Exception(message);
            }

            User user = _mapper.Map<User>(registerDTO);
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, BCrypt.Net.BCrypt.GenerateSalt());
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return _mapper.Map<RegisterDTO>(registerDTO);
            
            
        }

        public bool ValidateFields(RegisterDTO registerDTO, out string message)
        {
            message = "";

            if(!String.Equals(registerDTO.UserType,"Customer") && !String.Equals(registerDTO.UserType, "Seller") && !String.Equals(registerDTO.UserType, "Admin"))
            {
                message = "Invalid user type!";
                return false;
            }

            Regex emailRegex = new Regex(@"^([\w.-]+)@([\w-]+)((.(\w){2,3})+)$");

            if (!emailRegex.Match(registerDTO.Email).Success)
            {
                message = "Invalid email";
                return false;
            }

            if (String.IsNullOrWhiteSpace(registerDTO.Username))
            {
                message = "Username can't be empty";
                return false;
            }

            if (String.IsNullOrWhiteSpace(registerDTO.Password))
            {
                message = "Password can't be empty";
                return false;
            }

            if (String.IsNullOrWhiteSpace(registerDTO.Name))
            {
                message = "Name can't be empty";
                return false;
            }

            if (String.IsNullOrWhiteSpace(registerDTO.LastName))
            {
                message = "Last name can't be empty";
                return false;
            }

            if (String.IsNullOrWhiteSpace(registerDTO.Address))
            {
                message = "Address can't be empty";
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
