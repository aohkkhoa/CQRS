using Application.Features.BookFeatures.Queries;
using Application.Features.StorageFeatures.Queries;
using Domain.Models.DTO;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageContronller : ControllerBase
    {
        private readonly IMediator _mediator;

        public StorageContronller(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IEnumerable<StorageUnit>> GetAll()
        {
            return await _mediator.Send(new GetAllStorageQuery());
        }
    }
}
