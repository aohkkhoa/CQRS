using Application.Interfaces;
using Domain.Models.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.BookFeatures.Queries
{
    public class GetAllBookQuery : IRequest<IEnumerable<Book>>
    {
        
    }
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllBookQuery, IEnumerable<Book>>
        {
            private readonly IBookRepository _context;

        public GetAllProductsQueryHandler(IBookRepository context)
        {
            _context=context;
        }

        public async Task<IEnumerable<Book>> Handle(GetAllBookQuery query, CancellationToken cancellationToken)
            {
            var productList = await _context.GetBooks();
                if (productList == null)
                {
                    return null;
                }
                return productList.ToList();
            }
        }
}
