using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApplication.DTO.ItemDTO;
using WebApplication.Interfaces;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [Route("api/items")]
    [ApiController]
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;
        public ItemController(IItemService itemService) 
        {
            _itemService = itemService;
        }

        [HttpPost]
        [Authorize(Roles = "seller")]
        public IActionResult Post(CreateItemDTO createItemDTO)
        {
            try
            {
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
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }

        }
    }
}
