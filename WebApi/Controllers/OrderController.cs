using Application.Features.CategoryFeatures.Queries;
using Application.Features.OrderFeatures.Commands;
using Application.Features.OrderFeatures.Queries;
using Domain.Models.DTO;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpGet("{cusName}")]
        public async Task<IEnumerable<OrderInformation>> GetAll1(string cusName)
        {
            return await _mediator.Send(new GetAllOrderInformationQuery { CustomerName = cusName });
        }
        [HttpGet("total/{customerName}")]
        public async Task<float> SeeBeforePay(string customerName)
        {
            return await _mediator.Send(new GetTotalPriceWillPayQuery { CustomerName = customerName });
        }
        [HttpGet("orderWillPay/{customerName}")]
        public async Task<List<OrderInformation>> OrderBeforePay(string customerName)
        {
            return await _mediator.Send(new GetOrderWillPayQuery { CustomerName = customerName });
        }

        [HttpGet("paid/{cusName}")]
        public async Task<int> paid(string cusName)
        {
            return await _mediator.Send(new UpdateCheckPaidOrderComand { CustomerName = cusName });
        }
    }
}
