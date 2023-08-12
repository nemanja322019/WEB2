using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication.DTO;
using WebApplication.DTO.UserDTO;
using WebApplication.Interfaces;

namespace WebApplication.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id)
        {
            DisplayProfileDTO displayProfileDTO =  _userService.FindById(id);
            return Ok(displayProfileDTO);
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, UpdateProfileDTO updateProfileDTO)
        {
            DisplayProfileDTO displayProfileDTO = _userService.UpdateProfile(id, updateProfileDTO);
            return Ok(displayProfileDTO);
        }

        [HttpPut("{id}/change-password")]
        [Authorize]
        public IActionResult ChangePassword(int id,ChangePasswordDTO changePasswordDTO)
        {
            DisplayProfileDTO displayUserDTO = _userService.ChangePassword(id, changePasswordDTO);
            return Ok(displayUserDTO);
        }

        [HttpGet("sellers")]
        [Authorize(Roles ="admin")]
        public IActionResult GetSellers()
        {
            IEnumerable<DisplayProfileDTO> allSellers = _userService.GetSellers();
            return Ok(allSellers);
        }

        [HttpPut("{id}/verify")]
        [Authorize(Roles = "admin")]
        public IActionResult VerifySeller(int id, bool isAccepted)
        {
            DisplayProfileDTO displayProfileDTO = _userService.VerifySeller(id, isAccepted);
            return Ok(displayProfileDTO);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO loginDTO)
        {
            string token =  _userService.Login(loginDTO);
            return Ok(token);
        }

        [HttpPost("registration")]
        public IActionResult Register(RegisterDTO registerDTO)
        {
            var result = _userService.RegisterUser(registerDTO);
            return Ok(result);
        }

        
    }
}
