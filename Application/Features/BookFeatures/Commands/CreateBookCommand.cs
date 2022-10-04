using Application.Interfaces;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using Domain.Models.DTO;
using Domain.Models.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.BookFeatures.Commands
{
    public class CreateBookCommand : IRequest<Result<BookInformation>>
    {
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int CategoryId { get; set; }
        public int Quantity { get; set; }
        public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Result<BookInformation>>
        {
            private readonly IBookRepository _context;

            public CreateBookCommandHandler(IBookRepository context)
            {
                _context = context;
            }
            public async Task<Result<BookInformation>> Handle(CreateBookCommand command, CancellationToken cancellationToken)
            {
                if (_context.FindBookByName(command.Title)==0)
                {

                    var bookresult = await _context.AddQuantity(command.Title, command.Quantity);
                    return await Result<BookInformation>.SuccessAsync(bookresult, "just update quantity");
                }
                else
                {
                    var book = new Book();
                    book.Title = command.Title;
                    book.Author = command.Author;
                    book.Description = command.Description;
                    book.Price = command.Price;
                    book.CategoryId = command.CategoryId;
                    var bookresult = await _context.AddBook(book, command.Quantity);
                    return await Result<BookInformation>.SuccessAsync(bookresult, "add compelete");
                }
            }
        }
    }
}

