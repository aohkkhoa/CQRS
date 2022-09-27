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
    public class CreateBookCommand : IRequest<int>
    {
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int CategoryId { get; set; }
        public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, int>
        {
            private readonly IBookRepository _context;

            public CreateBookCommandHandler(IBookRepository context)
            {
                _context = context;
            }
            public async Task<int> Handle(CreateBookCommand command, CancellationToken cancellationToken)
            {

                /*using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var book = new Book();
                        book.Title = command.Title;
                        book.Author = command.Author;
                        book.Description = command.Description;
                        book.Title = command.Title;
                        book.CategoryId = command.CategoryId;
                        _context.Books.Add(book);
                        await _context.SaveChangesAsync();
                        throw new Exception();
                        var category = new Category()
                        {
                            CategoryName = "testTrans2"
                        };

                        _context.Categories.Add(category);
                        await _context.SaveChangesAsync();
                        transaction.Commit();
                        return book.Id;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine("Error occurred.");
                        return -2;
                    }

                }*/
                return -1;
            }
        }
    }
}

