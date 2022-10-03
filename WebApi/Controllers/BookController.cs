using Application.Features.BookFeatures.Commands;
using Application.Features.BookFeatures.Queries;
using Application.Features.CategoryFeatures.Queries;
using Domain.Models.DTO;
using Domain.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {



        private readonly IMediator _mediator;

        public BookController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Gets all Products.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<BookInformation>> GetAll()
        {
            return await _mediator.Send(new GetAllBookQuery());
        }


        //[HttpGet]
        //public async Task<IEnumerable<Book>> Get()
        //{
        //    var productList = await _context.Books.ToListAsync();
        //    if (productList == null) throw new Exception("loi");

        //    return productList.ToList();
        //}



        /// <summary>
        /// Creates a New Product.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateBookCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
