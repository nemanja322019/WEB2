using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplication.DTO.OrderDTO;
using WebApplication.Interfaces;

namespace WebApplication.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [Authorize(Roles = "customer")]
        public IActionResult Post(NewOrderDTO newOrderDTO)
        {
            try
            {
                DisplayOrderDTO displayOrderDTO = _orderService.NewOrder(newOrderDTO);
                return Ok(displayOrderDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }

        }

        [HttpPut("{id}/cancel")]
        [Authorize(Roles = "customer")]
        public IActionResult CancelOrder(int id)
        {
            try
            {
                DisplayOrderDTO displayOrderDTO = _orderService.CancelOrder(id);
                return Ok(displayOrderDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }

        }

        [HttpGet("{id}/delivered")]
        [Authorize(Roles = "customer")]
        public IActionResult GetDeliveredOrdersFromCustomer(int id)
        {
            try
            {
                IEnumerable<DisplayOrderDTO> orders = _orderService.GetDeliveredOrdersFromCustomer(id);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }

        }

        [HttpGet("{id}/ongoing")]
        [Authorize(Roles = "customer")]
        public IActionResult GetOngoingOrdersFromCustomer(int id)
        {
            try
            {
                IEnumerable<DisplayOrderDTO> orders = _orderService.GetOngoingOrdersFromCustomer(id);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }

        }

        [HttpGet("seller/{id}/new")]
        [Authorize(Roles = "seller")]
        public IActionResult GetNewOrdersFromSeller(int id)
        {
            try
            {
                IEnumerable<DisplayOrderDTO> orders = _orderService.GetNewOrdersFromSeller(id);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }

        }

        [HttpGet("seller/{id}/old")]
        [Authorize(Roles = "seller")]
        public IActionResult GetOldOrdersFromSeller(int id)
        {
            try
            {
                IEnumerable<DisplayOrderDTO> orders = _orderService.GetOldOrdersFromSeller(id);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }

        }

        [HttpGet("admin/all")]
        [Authorize(Roles = "admin")]
        public IActionResult GetAllOrders()
        {
            try
            {
                IEnumerable<DisplayOrderDTO> orders = _orderService.GetAllOrders();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }

        }

    }
}
