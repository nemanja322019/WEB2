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

        [HttpPost("registration")]
        public IActionResult Register(RegisterDTO registerDTO)
        {
            var result = _userService.RegisterUser(registerDTO);
            return Ok(result);
        }

    }
}
