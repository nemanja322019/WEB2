using WebApplication.DTO.ItemDTO;

namespace WebApplication.Interfaces
{
    public interface IItemService
    {
        public DisplayItemDTO CreateItem(CreateItemDTO createItemDTO);
        public DisplayItemDTO UpdateItem(int id,UpdateItemDTO updateItemDTO);
        public void DeleteItem(int id);
        public IEnumerable<DisplayItemDTO> GetItems();
        public IEnumerable<DisplayItemDTO> GetItemsFromSeller(int id);
    }
}
