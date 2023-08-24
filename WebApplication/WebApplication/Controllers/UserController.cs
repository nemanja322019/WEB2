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
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IWebHostEnvironment env, IConfiguration configuration)
        {
            _userService = userService;
            _env = env;
            _configuration = configuration;
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id)
        {
            try
            {
                DisplayProfileDTO displayProfileDTO =  _userService.FindById(id);
                return Ok(displayProfileDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }

        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, UpdateProfileDTO updateProfileDTO)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(updateProfileDTO.Image))
                {
                    string base64WithoutPrefix = updateProfileDTO.Image.Replace("data:image/jpeg;base64,", "");
                    byte[] imageBytes = Convert.FromBase64String(base64WithoutPrefix);

                    string webRootPath = _env.WebRootPath;

                    string imageName = Guid.NewGuid().ToString() + ".jpg";
                    string imagePath = Path.Combine(webRootPath, "slike", imageName);

                    System.IO.File.WriteAllBytes(imagePath, imageBytes);

                    string backendBaseUrl = _configuration.GetSection("UrlPath").Value;
                    string imageUrl = $"{backendBaseUrl}/slike/{imageName}";

                    updateProfileDTO.Image = imageUrl;
                }

                DisplayProfileDTO displayProfileDTO = _userService.UpdateProfile(id, updateProfileDTO);
                return Ok(displayProfileDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }

        }

        [HttpPut("{id}/change-password")]
        [Authorize]
        public IActionResult ChangePassword(int id,ChangePasswordDTO changePasswordDTO)
        {
            try
            {
                DisplayProfileDTO displayUserDTO = _userService.ChangePassword(id, changePasswordDTO);
                return Ok(displayUserDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }

        }

        [HttpGet("sellers")]
        [Authorize(Roles ="admin")]
        public IActionResult GetSellers()
        {
            try
            {
                IEnumerable<DisplayProfileDTO> allSellers = _userService.GetSellers();
                return Ok(allSellers);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }

        }

        [HttpPut("{id}/verify/{isAccepted}")]
        [Authorize(Roles = "admin")]
        public IActionResult VerifySeller(int id, bool isAccepted)
        {
            try
            {
                DisplayProfileDTO displayProfileDTO = _userService.VerifySeller(id, isAccepted);
                return Ok(displayProfileDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO loginDTO)
        {
            try
            {
                string token = _userService.Login(loginDTO);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(new {Error = ex.Message});
            }
        }

        [HttpPost("registration")]
        public IActionResult Register(RegisterDTO registerDTO)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(registerDTO.Image))
                {
                    string base64WithoutPrefix = registerDTO.Image.Replace("data:image/jpeg;base64,", "");
                    byte[] imageBytes = Convert.FromBase64String(base64WithoutPrefix);

                    string webRootPath = _env.WebRootPath;

                    string imageName = Guid.NewGuid().ToString() + ".jpg";
                    string imagePath = Path.Combine(webRootPath, "slike", imageName);

                    System.IO.File.WriteAllBytes(imagePath, imageBytes);

                    string backendBaseUrl = _configuration.GetSection("UrlPath").Value;
                    string imageUrl = $"{backendBaseUrl}/slike/{imageName}";

                    registerDTO.Image = imageUrl;
                }


                _userService.RegisterUser(registerDTO);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginDTO googleLoginDTO)
        {
            try
            {
                string token = await _userService.GoogleLogin(googleLoginDTO);
                return Ok(token);
            }
            catch(Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }



    }
}
