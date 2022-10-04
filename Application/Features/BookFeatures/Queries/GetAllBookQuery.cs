using Application.Interfaces;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using Domain.Models.DTO;
using Domain.Models.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.BookFeatures.Queries
{
    public class GetAllBookQuery : IRequest<Result<List<BookInformation>>>
    {
        
    }
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllBookQuery, Result<List<BookInformation>>>
        {
            private readonly IBookRepository _context;

        public GetAllProductsQueryHandler(IBookRepository context)
        {
            _context=context;
        }

        public async Task<Result<List<BookInformation>>> Handle(GetAllBookQuery query, CancellationToken cancellationToken)
            {

            var productList = await _context.GetBooks();
                if (productList == null)
                {
                    return null;
                }
                return  await Result<List<BookInformation>>.SuccessAsync(productList); ;
            }
        }
}
