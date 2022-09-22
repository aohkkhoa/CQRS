using Application.Features.CategoryFeatures.Queries;
using Domain.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IEnumerable<Category>> GetAll1()
        {
            return await _mediator.Send(new GetAllCategoryQuery());
        }
    }
}
