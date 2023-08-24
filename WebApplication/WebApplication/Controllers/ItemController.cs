using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApplication.DTO.ItemDTO;
using WebApplication.DTO.UserDTO;
using WebApplication.Interfaces;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [Route("api/items")]
    [ApiController]
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public ItemController(IItemService itemService, IConfiguration configuration, IWebHostEnvironment env) 
        {
            _itemService = itemService;
            _configuration = configuration;
            _env = env;
        }

        [HttpPost]
        [Authorize(Roles = "seller")]
        public IActionResult Post(CreateItemDTO createItemDTO)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(createItemDTO.Image))
                {
                    string base64WithoutPrefix = createItemDTO.Image.Replace("data:image/jpeg;base64,", "");
                    byte[] imageBytes = Convert.FromBase64String(base64WithoutPrefix);

                    string webRootPath = _env.WebRootPath;

                    string imageName = Guid.NewGuid().ToString() + ".jpg";
                    string imagePath = Path.Combine(webRootPath, "slike", imageName);

                    System.IO.File.WriteAllBytes(imagePath, imageBytes);

                    string backendBaseUrl = _configuration.GetSection("UrlPath").Value;
                    string imageUrl = $"{backendBaseUrl}/slike/{imageName}";

                    createItemDTO.Image = imageUrl;
                }

                DisplayItemDTO displayItemDTO = _itemService.CreateItem(createItemDTO);
                return Ok(displayItemDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }

        }

        [HttpPut("{id}")]
        [Authorize(Roles = "seller")]
        public IActionResult Put(int id, UpdateItemDTO updateItemDTO)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(updateItemDTO.Image))
                {
                    string base64WithoutPrefix = updateItemDTO.Image.Replace("data:image/jpeg;base64,", "");
                    byte[] imageBytes = Convert.FromBase64String(base64WithoutPrefix);

                    string webRootPath = _env.WebRootPath;

                    string imageName = Guid.NewGuid().ToString() + ".jpg";
                    string imagePath = Path.Combine(webRootPath, "slike", imageName);

                    System.IO.File.WriteAllBytes(imagePath, imageBytes);

                    string backendBaseUrl = _configuration.GetSection("UrlPath").Value;
                    string imageUrl = $"{backendBaseUrl}/slike/{imageName}";

                    updateItemDTO.Image = imageUrl;
                }

                DisplayItemDTO displayItemDTO = _itemService.UpdateItem(id, updateItemDTO);
                return Ok(displayItemDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "seller")]
        public IActionResult Delete(int id)
        {
            try
            {
                _itemService.DeleteItem(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }

        }

        [HttpGet]
        [Authorize]
        public IActionResult GetItems()
        {
            try
            {
                IEnumerable<DisplayItemDTO> items = _itemService.GetItems();
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }

        }

        [HttpGet("seller/{id}")]
        [Authorize(Roles = "seller")]
        public IActionResult GetItemsForSeller(int id)
        {
            try
            {
                IEnumerable<DisplayItemDTO> items = _itemService.GetItemsFromSeller(id);

                foreach(DisplayItemDTO item in items)
                {
                    
                }

                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }

        }
    }
}
