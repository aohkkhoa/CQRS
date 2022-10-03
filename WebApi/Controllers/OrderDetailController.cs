﻿using Application.Features.OrderFeatures.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderDetailController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create(UpdateQuantityCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteOrderDetailCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
