using AutoMapper;
using System.Net.Http.Headers;
using WebApplication.DTO.ItemDTO;
using WebApplication.DTO.UserDTO;
using WebApplication.Infrastructure;
using WebApplication.Interfaces;
using WebApplication.Models;

namespace WebApplication.Services
{
    public class ItemService : IItemService
    {
        private readonly IMapper _mapper;
        private readonly WebApplicationDbContext _dbContext;

        public ItemService(IMapper mapper, WebApplicationDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public DisplayItemDTO CreateItem(CreateItemDTO createItemDTO)
        {
            string message;
            if (!ValidateFields(createItemDTO.ItemName, createItemDTO.Description, createItemDTO.Price, createItemDTO.Amount, out message))
            {
                throw new Exception(message);
            }

            User seller = _dbContext.Users.Find(createItemDTO.SellerId);
            if (seller == null)
            {
                throw new Exception("Seller not found!");
            }

            if(seller.VerificationStatus != Enums.VerificationStatus.ACCEPTED)
            {
                throw new Exception("Seller is not verified!");
            }

            Item item = _mapper.Map<Item>(createItemDTO);
            item.Seller = seller;
            _dbContext.Items.Add(item);
            _dbContext.SaveChanges();

            return _mapper.Map<DisplayItemDTO>(item);
        }

        public DisplayItemDTO UpdateItem(int id, UpdateItemDTO updateItemDTO)
        {
            Item item = _dbContext.Items.Find(id);
            if (item == null)
            {
                throw new Exception("Item does not exist!");
            }

            string message;
            if (!ValidateFields(updateItemDTO.ItemName, updateItemDTO.Description, updateItemDTO.Price, updateItemDTO.Amount, out message))
            {
                throw new Exception(message);
            }
            
            item.ItemName = updateItemDTO.ItemName;
            item.Description = updateItemDTO.Description;
            item.Price = updateItemDTO.Price;
            item.Amount = updateItemDTO.Amount;
            if (!String.IsNullOrEmpty(updateItemDTO.Image))
            {
                item.Image = updateItemDTO.Image;
            }
            _dbContext.SaveChanges();

            return _mapper.Map<DisplayItemDTO>(item);
        }

        public void DeleteItem(int id)
        {
            Item item = _dbContext.Items.Find(id);
            if (item == null)
            {
                throw new Exception("Item does not exist!");
            }
            _dbContext.Remove(item);
            _dbContext.SaveChanges();
        }

        public bool ValidateFields(string ItemName, string Description,double Price, int Amount, out string message)
        {
            message = "";

            if (String.IsNullOrWhiteSpace(ItemName))
            {
                message = "Item name can't be empty!";
                return false;
            }
            if (String.IsNullOrWhiteSpace(Description))
            {
                message = "Description can't be empty!";
                return false;
            }
            if (Price < 0)
            {
                message = "Price can't be less than 0!";
                return false;
            }
            if (Amount < 1)
            {
                message = "Amount can't be less than 1!";
                return false;
            }

            return true;
        }

        public IEnumerable<DisplayItemDTO> GetItems()
        {
            IEnumerable<Item> temp = _dbContext.Items;

            IEnumerable<DisplayItemDTO> allItems = _mapper.Map<IEnumerable<DisplayItemDTO>>(temp);

            return allItems;
        }

        public IEnumerable<DisplayItemDTO> GetItemsFromSeller(int id)
        {
            IEnumerable<Item> temp = _dbContext.Items.Where(item => item.SellerId == id);

            IEnumerable<DisplayItemDTO> allItemsForSeller = _mapper.Map<IEnumerable<DisplayItemDTO>>(temp);

            return allItemsForSeller;
        }

    }
}
