using Application.Features.StorageFeatures.Queries;
using Domain.Models.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Wrapper;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StorageController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<Result<IEnumerable<StorageUnit>>> GetAll()
        {
            return await _mediator.Send(new GetAllStorageQuery());
        }
    }
}