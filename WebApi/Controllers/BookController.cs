using Application.Features.BookFeatures.Commands.Create;
using Application.Features.BookFeatures.Queries;
using Application.Interfaces;
using Domain.Models.DTO;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Constant.Menu;
using Shared.Wrapper;
using WebApi.Attribute;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IValidator<CreateBookCommand> _validator;
        private readonly IMediator _mediator;

        public BookController(IMediator mediator, IValidator<CreateBookCommand> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        //[Authorize(Policy= "Ex1")]

        [HttpGet]
        [AuthorizeUser(MenuConstants.Menu1)]
        public async Task<Result<List<BookInformation>>> GetAll()
        {
            return await _mediator.Send(new GetAllBookQuery());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookCommand command)
        {
            ValidationResult result = await _validator.ValidateAsync(command);

            if (!result.IsValid)
            {
                // Copy the validation results into ModelState.
                // ASP.NET uses the ModelState collection to populate 
                // error messages in the View.
                result.AddToModelState(this.ModelState);
                // re-render the view when validation failed.
                return result.IsValid ? Ok(result) : BadRequest(result);
            }

            return Ok(await _mediator.Send(command));
        }
    }
}