using Application.Features.BookFeatures.Queries;
using Application.Interfaces;
using Domain.Models.Entities;
using MediatR;
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
        private readonly ICategoryRepository _context;

        public GetAllcategoriesQueryHandler(ICategoryRepository context)
        {
            _context=context;
        }

        public Task<IEnumerable<Category>> Handle(GetAllCategoryQuery query, CancellationToken cancellationToken)
        {
            var productList =  _context.GetAllCategories();
            if (productList == null)
            {
                return null;
            }
            return Task.FromResult(productList);
        }

    
    }
}
