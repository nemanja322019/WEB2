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
            DisplayItemDTO displayItemDTO = _itemService.CreateItem(createItemDTO);
            return Ok(displayItemDTO);
        }

        [HttpPost("{id}")]
        [Authorize(Roles = "seller")]
        public IActionResult Put(int id, UpdateItemDTO updateItemDTO)
        {
            DisplayItemDTO displayItemDTO = _itemService.UpdateItem(id, updateItemDTO);
            return Ok(displayItemDTO);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "seller")]
        public IActionResult Delete(int id)
        {
            _itemService.DeleteItem(id);
            return NoContent();
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetItems()
        {
            IEnumerable<DisplayItemDTO> items = _itemService.GetItems();
            return Ok(items);
        }

        [HttpGet("seller/{id}")]
        [Authorize(Roles = "seller")]
        public IActionResult GetItemsForSeller(int id)
        {
            IEnumerable<DisplayItemDTO> items = _itemService.GetItemsFromSeller(id);
            return Ok(items);
        }
    }
}
