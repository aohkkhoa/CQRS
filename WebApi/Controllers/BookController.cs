using Application.Features.BookFeatures.Commands.Create;
using Application.Features.BookFeatures.Queries;
using Application.Features.CategoryFeatures.Queries;
using Application.Validators.Features.Books.Commands;
using BookManagement2.Models.Entities;
using Domain.Models.DTO;
using Domain.Models.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using Shared.Wrapper;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private IValidator<CreateBookCommand> _validator;
        private readonly IMediator _mediator;

        public BookController(IMediator mediator, IValidator<CreateBookCommand> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        [Authorize(Roles="Admin")]
        [HttpGet]
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
