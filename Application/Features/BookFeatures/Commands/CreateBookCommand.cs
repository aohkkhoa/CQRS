using Application.Interfaces;
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
    public class CreateBookCommand : IRequest<string>
    {
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int CategoryId { get; set; }
        public int Quantity { get; set; }
        public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, string>
        {
            private readonly IBookRepository _context;

            public CreateBookCommandHandler(IBookRepository context)
            {
                _context = context;
            }
            public async Task<string> Handle(CreateBookCommand command, CancellationToken cancellationToken)
            {
                if (_context.FindBookByName(command.Title)==0)
                {
                    return await _context.AddQuantity(command.Title, command.Quantity);

                }
                else
                {
                    var book = new Book();
                    book.Title = command.Title;
                    book.Author = command.Author;
                    book.Description = command.Description;
                    book.Price = command.Price;
                    book.CategoryId = command.CategoryId;
                    await _context.AddBook(book, command.Quantity);
                    return "just add book";
                }
            }
        }
    }
}

