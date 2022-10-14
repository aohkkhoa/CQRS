using Application.Features.PermissionFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using IResult = Shared.Wrapper.IResult;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PermissionController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IResult> GetAll()
        {
            return await _mediator.Send(new GetAllPermissionQuery());
        }
    }
}