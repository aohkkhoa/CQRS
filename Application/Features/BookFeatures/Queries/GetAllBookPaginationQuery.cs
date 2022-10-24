using Application.Interfaces;
using Domain.Models.DTO;
using MediatR;
using Shared.Wrapper;

namespace Application.Features.BookFeatures.Queries
{
    public class GetAllBookPaginationQuery : IRequest<Result<IEnumerable<BookInformation>>>
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }

    public class GetAllBookPaginationQueryHandle : IRequestHandler<GetAllBookPaginationQuery, Result<IEnumerable<BookInformation>>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IStorageRepository _storageRepository;

        public GetAllBookPaginationQueryHandle(IStorageRepository storageRepository, ICategoryRepository categoryRepository, IBookRepository bookRepository)
        {
            _storageRepository = storageRepository;
            _categoryRepository = categoryRepository;
            _bookRepository = bookRepository;
        }

        public Task<Result<IEnumerable<BookInformation>>> Handle(GetAllBookPaginationQuery request, CancellationToken cancellationToken)
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
            if (request.Page is null) request.Page = 1;
            if (request.PageSize is null) request.PageSize = 5;
            int skip = (request.Page.Value - 1) * request.PageSize.Value;
            var listBookInformationPagination = listBookInformation
                                                .OrderByDescending(b => b.BookId)
                                                .Skip(skip)
                                                .Take(request.PageSize.Value);
            return Result<IEnumerable<BookInformation>>.SuccessAsync(listBookInformationPagination);
        }
    }
}