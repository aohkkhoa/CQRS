using Application.Interfaces;
using Domain.Models.DTO;
using Domain.Models.Entities;
using MediatR;
using Shared.Wrapper;

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

        public async Task<Result<BookInformation>> Handle(CreateBookCommand command,
            CancellationToken cancellationToken)
        {
            if (_bookRepository.FindBookByName(command.createBookViewModel.Title) == 0)
            {
                var a = _bookRepository.GetBookByAuthor(command.createBookViewModel.Author);
                if (a.Result.Succeeded)
                {
                    return await Result<BookInformation>.FailAsync("book of this author is exist");
                }

                var bookResult = await _bookRepository.AddQuantity(command.createBookViewModel.Title,
                    command.createBookViewModel.Quantity);
                return await Result<BookInformation>.SuccessAsync(bookResult, "Just Update Quantity");
            }

            var book = new Book
            {
                Title = command.createBookViewModel.Title,
                Author = command.createBookViewModel.Author,
                Description = command.createBookViewModel.Description,
                Price = command.createBookViewModel.Price,
                CategoryId = command.createBookViewModel.CategoryId
            };
            var result = await _bookRepository.AddBook(book, command.createBookViewModel.Quantity);
            return await Result<BookInformation>.SuccessAsync(result, "Add Complete");
        }
    }
}