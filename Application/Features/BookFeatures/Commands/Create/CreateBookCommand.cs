using Application.Interfaces;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using Domain.Models.DTO;
using Domain.Models.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Application.Features.BookFeatures.Commands.Create
{
    public class CreateBookCommand : IRequest<Result<BookInformation>>
    {
        public CreateBookViewModel createBookViewModel { get; set; }
    }
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Result<BookInformation>>
    {
        private readonly IBookRepository _bookRepository;

        public CreateBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task<Result<BookInformation>> Handle(CreateBookCommand command, CancellationToken cancellationToken)
        {
            if (_bookRepository.FindBookByName(command.createBookViewModel.Title) == 0)
            {
                var bookresult = await _bookRepository.AddQuantity(command.createBookViewModel.Title, command.createBookViewModel.Quantity);
                return await Result<BookInformation>.SuccessAsync(bookresult, "Just Update Quantity");
            }
            else
            {
                var book = new Book();
                book.Title = command.createBookViewModel.Title;
                book.Author = command.createBookViewModel.Author;
                book.Description = command.createBookViewModel.Description;
                book.Price = command.createBookViewModel.Price;
                book.CategoryId = command.createBookViewModel.CategoryId;
                var result = await _bookRepository.AddBook(book, command.createBookViewModel.Quantity);
                return await Result<BookInformation>.SuccessAsync(result, "Add Compelete");
            }
        }
    }
}


