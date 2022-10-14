using Application.Exceptions;
using Application.Interfaces;
using Domain.Models.DTO;
using MediatR;
using Shared.Wrapper;

namespace Application.Features.BookFeatures.Queries
{
    public class GetAllBookQuery : IRequest<Result<List<BookInformation>>>
    {
    }

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllBookQuery, Result<List<BookInformation>>>
    {
        private readonly IBookRepository _bookRepository;

        public GetAllProductsQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Result<List<BookInformation>>> Handle(GetAllBookQuery query,
            CancellationToken cancellationToken)
        {
            var productList = await _bookRepository.GetBooks();
            if (!productList.Any())
            {
                throw new ApiException("null roi coi lai du lieu trong sql");
            }

            return await Result<List<BookInformation>>.SuccessAsync(productList);
        }
    }
}