using Application.Features.BookFeatures.Commands.Create;
using Application.Features.BookFeatures.Commands.Delete;
using Application.Features.BookFeatures.Commands.Update;
using Application.Features.BookFeatures.Queries;
using Domain.Models.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Constant.Menu;
using Shared.Wrapper;
using WebApi.Attribute;
using IResult = Shared.Wrapper.IResult;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : BaseController
    {
        public BookController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [AuthorizeUser(MenuConstants.Menu1)]
        public async Task<Result<IEnumerable<BookInformation>>> GetAll()
        {
            return await _mediator.Send(new GetAllBookQuery());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteLogic(DeleteBookCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet("Pagination")]
        public async Task<Result<IEnumerable<BookInformation>>> Pagination(int? page, int? pageSize, string? categoryName, string strings)
        {
            return await _mediator.Send(new GetAllBookPaginationQuery { Page = page, PageSize = pageSize, CategoryName = categoryName, strings = strings });
        }

        [HttpPut("Edit-Quantity")]
        public async Task<IResult> EditQuantity(int bookId, int quantity)
        {
            return await _mediator.Send(new UpdateQuantityOfBookInStorage { BookId = bookId, Quantity = quantity });
        }
    }
}