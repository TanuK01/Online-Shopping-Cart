using AutoMapper;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using eShoppingZoneAPI.DTO;
using eShoppingZoneAPI.Errors;
using eShoppingZoneAPI.Extensions;
using Infrastructure.Services;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Security.Claims;

namespace eShoppingZoneAPI.Controllers
{
    [Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var email = HttpContext.User.RetrieveEmailFromPrinciple();
            var address = _mapper.Map<AddressDto, Address>(orderDto.ShipToAddress);
            var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);
                        
            if (order == null){
                return BadRequest(new ApiResponse(400, "Problem creating order"));
            } 
            else{
                var mail = new MimeMessage();
                mail.From.Add(MailboxAddress.Parse("subhansshukla73@gmail.com"));
                mail.To.Add(MailboxAddress.Parse(email));
                mail.Subject = "Welcome to eShoppingZone " + address.FirstName;
                mail.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = email + ",We have created your order successfully. Wish you a happy experience" };

                using var SMTP = new SmtpClient();
                SMTP.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                SMTP.Authenticate("subhans.ss.73.ss@gmail.com", "rygyfxcexaooxzqt");
                SMTP.Send(mail);
                SMTP.Disconnect(true);
            }
            
                        
            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetOrdersForUser()
        {
            var email = HttpContext.User.RetrieveEmailFromPrinciple();
            var orders = await _orderService.GetOrdersForUserAsync(email);
            return Ok(_mapper.Map<IReadOnlyList<Order>,IReadOnlyList<OrderToReturnDto>>(orders));
        }

        [HttpGet("id")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdForUser(int id)
        {
            var email = HttpContext.User.RetrieveEmailFromPrinciple();
            var order = await _orderService.GetOrderByIdAsync(id, email);
            if (order == null) return NotFound(new ApiResponse(404));
            return _mapper.Map<Order, OrderToReturnDto>(order);
        }

        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            return Ok(await _orderService.GetDeliveryMethodsAsync());
        }
    }
}
