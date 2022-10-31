using Application.Interfaces;
using Domain.Models.DTO;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shared.Wrapper;
using System.Reflection;

namespace Application.Features.BookFeatures.Queries
{
    public class GetAllBookPaginationQuery : IRequest<Result<IEnumerable<BookInformation>>>
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public string? CategoryName { get; set; }
        public string strings { get; set; }
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
                                      where (b.IsDeleted == false)
                                      && (string.IsNullOrEmpty(request.CategoryName) || c.CategoryName.Contains(request.CategoryName))
                                      orderby b.Id descending
                                      select new BookInformation()
                                      {
                                          BookId = b.Id,
                                          Category = c.CategoryName,
                                          Title = b.Title,
                                          Author = b.Author,
                                          Quantity = s.Quantity
                                      };
            IOrderedEnumerable<BookInformation> orderedList = listBookInformation.OrderByDescending(e => e.BookId);

            foreach (var item in request.strings.Split(','))
            {
                var mapPropertyOrder = new List<string>(item.Split('='));
                if (item is not null)
                {
                    if (mapPropertyOrder[1] == "desc")
                        orderedList = orderedList.ThenByDescending(e => e.GetType().GetProperty(mapPropertyOrder[0]));
                    else
                        orderedList = orderedList.ThenBy(e => e.GetType().GetProperty(mapPropertyOrder[0]));
                }
            }

            /* listBookInformation.OrderByDescending(a => a.BookId);
             foreach (string propname in queryString.Split(','))
                 listBookInformation.(x => GetPropertyValue(x, propname));*/

            if (request.Page is null) request.Page = 1;
            if (request.PageSize is null) request.PageSize = 5;
            int skip = (request.Page.Value - 1) * request.PageSize.Value;
            var listBookInformationPagination = orderedList
                                                .OrderByDescending(b => b.BookId)
                                                .Skip(skip)
                                                .Take(request.PageSize.Value)
                                                ;

            return Result<IEnumerable<BookInformation>>.SuccessAsync(listBookInformationPagination);
        }
    }
}