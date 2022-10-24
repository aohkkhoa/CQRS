using Application.Exceptions;
using Application.Interfaces;
using Domain.Models.DTO;
using Domain.Models.Entities;
using MediatR;
using Shared.Wrapper;

namespace Application.Features.BookFeatures.Commands.Create
{
    public class CreateBookCommand : IRequest<Result<BookInformation>>
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int CategoryId { get; set; }
        public int Quantity { get; set; }
    }

    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Result<BookInformation>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IStorageRepository _storageRepository;

        public CreateBookCommandHandler(IBookRepository bookRepository, ICategoryRepository categoryRepository, IStorageRepository storageRepository)
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
            _storageRepository = storageRepository;
        }

        public async Task<Result<BookInformation>> Handle(CreateBookCommand command, CancellationToken cancellationToken)
        {
            var bookByTitle = _bookRepository.Entities.FirstOrDefault(b => b.Title == command.Title);
            if (bookByTitle is not null)
            {
                var bookByAuthor = _bookRepository.Entities.FirstOrDefault(b => b.Author == command.Author);
                if (bookByAuthor is not null)
                {
                    return await Result<BookInformation>.FailAsync("Book of this author is exist !");
                }
            }

            var newBook = new Book
            {
                Title = command.Title,
                Author = command.Author,
                Description = command.Description,
                Price = command.Price,
                CategoryId = command.CategoryId
            };
            _bookRepository.Insert(newBook);
            await _bookRepository.Save();
            var categoryOfNewBook = _categoryRepository.Entities.FirstOrDefault(c => c.CategoryId == newBook.CategoryId);
            if (categoryOfNewBook is null)
            {
                throw new ApiException("Category Not Found !");
            }

            var storageOfNewBook = new Storage()
            {
                BookId = newBook.Id,
                Quantity = command.Quantity
            };
            _storageRepository.Insert(storageOfNewBook);
            await _storageRepository.Save();
            return await Result<BookInformation>.SuccessAsync("Add Complete !");
        }
    }
}