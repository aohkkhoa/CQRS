using Application.Features.CategoryFeatures.Queries;
using Application.Features.StorageFeatures.Commands;
using Domain.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Wrapper;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<Result<IEnumerable<Category>>> GetAll()
        {
            return await _mediator.Send(new GetAllCategoryQuery());
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteCategoryCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}