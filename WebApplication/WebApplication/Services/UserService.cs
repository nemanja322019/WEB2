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

        public UserService(IMapper mapper, WebApplicationDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public RegisterDTO RegisterUser(RegisterDTO registerDTO)
        {
            string message;
            if (!ValidateFields(registerDTO, out message))
            {
                throw new Exception(message);
            }

            if (registerDTO.UserType is "Customer")
            {
                Customer customer = _mapper.Map<Customer>(registerDTO);
                customer.Password = BCrypt.Net.BCrypt.HashPassword(customer.Password, BCrypt.Net.BCrypt.GenerateSalt());
                _dbContext.Customers.Add(customer);
                _dbContext.SaveChanges();
                return _mapper.Map<RegisterDTO>(registerDTO);
            }
            else if (registerDTO.UserType is "Seller")
            {
                Seller seller = _mapper.Map<Seller>(registerDTO);
                seller.Password = BCrypt.Net.BCrypt.HashPassword(seller.Password, BCrypt.Net.BCrypt.GenerateSalt());
                _dbContext.Sellers.Add(seller);
                _dbContext.SaveChanges();
                return _mapper.Map<RegisterDTO>(registerDTO);
            }
            else
            {
                Admin admin = _mapper.Map<Admin>(registerDTO);
                admin.Password = BCrypt.Net.BCrypt.HashPassword(admin.Password, BCrypt.Net.BCrypt.GenerateSalt());
                _dbContext.Admins.Add(admin);
                _dbContext.SaveChanges();
                return _mapper.Map<RegisterDTO>(registerDTO);
            }
            
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

        public LoginDTO SearchUser(string username)
        {
            LoginDTO loginDTO = new LoginDTO();
            loginDTO.Username = "";
            loginDTO.Password = "";

            var customer = _dbContext.Customers.FirstOrDefault(c => c.Username == username);
            if (customer != null)
            {
                loginDTO.Username = customer.Username;
                loginDTO.Password = customer.Password;
                return loginDTO;
            }

            var seller = _dbContext.Sellers.FirstOrDefault(s => s.Username == username);
            if (seller != null)
            {
                loginDTO.Username = seller.Username;
                loginDTO.Password = seller.Password;
                return loginDTO;
            }

            var admin = _dbContext.Admins.FirstOrDefault(a => a.Username == username);      
            if (admin != null)
            {
                loginDTO.Username = admin.Username;
                loginDTO.Password = admin.Password;
                return loginDTO;
            }

            return loginDTO;

        }

        public bool UserExists(string username)
        {
            return _dbContext.Admins.Any(a => a.Username == username) || _dbContext.Sellers.Any(s => s.Username == username) || _dbContext.Customers.Any(c => c.Username == username);
        }

        public bool EmailExists(string email)
        {
            return _dbContext.Admins.Any(a => a.Email == email) || _dbContext.Sellers.Any(s => s.Email == email) || _dbContext.Customers.Any(c => c.Email == email);
        }

    }
}
