using Application.Interfaces;
using Domain.Models.DTO;
using Domain.Models.Entities;
using MediatR;
using Shared.Wrapper;

namespace Application.Features.BookFeatures.Commands.Create
{
    public class CreateBookCommand : IRequest<Result<BookInformation>>
    {
        public CreateBookViewModel CreateBookViewModel { get; set; }
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
            if (_bookRepository.FindBookByName(command.CreateBookViewModel.Title) == 0)
            {
                var bookByAuthor = await _bookRepository.GetBookByAuthor(command.CreateBookViewModel.Author);
                if (bookByAuthor.Succeeded)
                {
                    return await Result<BookInformation>.FailAsync("book of this author is exist");
                }

                var bookResult = await _bookRepository.AddQuantity(command.CreateBookViewModel.Title,
                    command.CreateBookViewModel.Quantity);
                return await Result<BookInformation>.SuccessAsync(bookResult, "Just Update Quantity");
            }

            var book = new Book
            {
                Title = command.CreateBookViewModel.Title,
                Author = command.CreateBookViewModel.Author,
                Description = command.CreateBookViewModel.Description,
                Price = command.CreateBookViewModel.Price,
                CategoryId = command.CreateBookViewModel.CategoryId
            };
            var result = await _bookRepository.AddBook(book, command.CreateBookViewModel.Quantity);
            return await Result<BookInformation>.SuccessAsync(result, "Add Complete");
        }
    }
}