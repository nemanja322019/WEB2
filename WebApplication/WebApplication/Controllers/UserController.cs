using Microsoft.AspNetCore.Mvc;
using WebApplication.DTO;
using WebApplication.Interfaces;

namespace WebApplication.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO loginDTO)
        {
            string token =  _userService.Login(loginDTO);
            return Ok();
        }

        [HttpPost("registration")]
        public IActionResult Register(RegisterDTO registerDTO)
        {
            var result = _userService.RegisterUser(registerDTO);
            return Ok(result);
        }

    }
}
