using Application.Interfaces;
using Domain.Models.DTO;
using MediatR;
using Shared.Wrapper;

namespace Application.Features.BookFeatures.Queries
{
    public class GetAllBookQuery : IRequest<Result<IEnumerable<BookInformation>>>
    {
    }

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllBookQuery, Result<IEnumerable<BookInformation>>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IStorageRepository _storageRepository;

        public GetAllProductsQueryHandler(IBookRepository bookRepository, ICategoryRepository categoryRepository, IStorageRepository storageRepository)
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
            _storageRepository = storageRepository;
        }

        public async Task<Result<IEnumerable<BookInformation>>> Handle(GetAllBookQuery query, CancellationToken cancellationToken)
        {
            var listBookInformation = from b in _bookRepository.Entities
                                      join c in _categoryRepository.Entities on b.CategoryId equals c.CategoryId
                                      join s in _storageRepository.Entities on b.Id equals s.BookId
                                      where b.IsDeleted == false
                                      orderby b.Id descending
                                      select new BookInformation()
                                      {
                                          BookId = b.Id,
                                          Category = c.CategoryName,
                                          Title = b.Title,
                                          Author = b.Author,
                                          Quantity = s.Quantity
                                      };
            return await Result<IEnumerable<BookInformation>>.SuccessAsync(listBookInformation);
        }
    }
}