using Domain.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
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
            private readonly ApplicationDbContext _context;

        public GetAllProductsQueryHandler(ApplicationDbContext context)
        {
            _context=context;
        }

        public async Task<IEnumerable<Book>> Handle(GetAllBookQuery query, CancellationToken cancellationToken)
            {
                var productList = await _context.Books.ToListAsync();
                if (productList == null)
                {
                    return null;
                }
                return productList.ToList();
            }
        }
}
