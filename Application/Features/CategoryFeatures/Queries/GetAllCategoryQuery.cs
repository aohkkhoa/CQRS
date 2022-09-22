using Application.Features.BookFeatures.Queries;
using Domain.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CategoryFeatures.Queries
{
    public class GetAllCategoryQuery : IRequest<IEnumerable<Category>>
    {

    }
    public class GetAllcategoriesQueryHandler : IRequestHandler<GetAllCategoryQuery, IEnumerable<Category>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllcategoriesQueryHandler(ApplicationDbContext context)
        {
            _context=context;
        }

        public async Task<IEnumerable<Category>> Handle(GetAllCategoryQuery query, CancellationToken cancellationToken)
        {
            var productList = await _context.Categories.ToListAsync();
            if (productList == null)
            {
                return null;
            }
            return productList.ToList();
        }
    }
}
