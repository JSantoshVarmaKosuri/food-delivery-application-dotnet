using fdcommon.Domain.Dtos.Order;
using fdorder.Controllers.Entities;
using fdorder.Domain.Ports;
using Microsoft.AspNetCore.Mvc;

namespace fdorder.gateway
{
    [Route("service/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderDomainService _orderDomainService;
        public OrderController(IOrderDomainService orderDomainService)
        {
            this._orderDomainService = orderDomainService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            Console.WriteLine("Id: " + id);
            try
            {
                OrderDto orderDto = await this._orderDomainService.GetOrder(id);
                List<OrderDto> orderDtos = new List<OrderDto>();
                orderDtos.Add(orderDto);

                return Ok(new
                {
                    isSuccess = true,
                    data = orderDtos,
                    error = ""
                });
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    isSuccess = false,
                    data = new List<OrderDto>(),
                    error = e.Message.ToString()
                });
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] OrderModel order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                try
                {
                    OrderDto newOrderDto = await this._orderDomainService.CreateOrder(
                        order.customerId,
                        order.restarentId,
                        order.orderItems);

                    return Created(
                        "/Order/Create",
                        new
                        {
                            IsSucces = true,
                            Order = newOrderDto,
                            Error = ""
                        });
                }
                catch (Exception e)
                {
                    return Ok(new
                    {
                        IsSucces = false,
                        Error = e.Message.ToString()
                    });
                }
            }
        }
    }
}