using Application.Interfaces;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using Domain.Models.DTO;
using MediatR;

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
            _bookRepository=bookRepository;
        }

        public async Task<Result<List<BookInformation>>> Handle(GetAllBookQuery query, CancellationToken cancellationToken)
        {

            var productList = await _bookRepository.GetBooks();
            if (productList == null)
            {
                return null;
            }
            return await Result<List<BookInformation>>.SuccessAsync(productList);
        }
    }
}
