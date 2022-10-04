using Application.Interfaces;
using Domain.Models.Entities;
using MediatR;

namespace Application.Features.CategoryFeatures.Queries
{
    public class GetAllCategoryQuery : IRequest<IEnumerable<Category>>
    {

    }
    public class GetAllcategoriesQueryHandler : IRequestHandler<GetAllCategoryQuery, IEnumerable<Category>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetAllcategoriesQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository=categoryRepository;
        }

        public Task<IEnumerable<Category>> Handle(GetAllCategoryQuery query, CancellationToken cancellationToken)
        {
            var productList = _categoryRepository.GetAllCategories();
            if (productList == null)
            {
                return null;
            }
            return Task.FromResult(productList);
        }


    }
}
