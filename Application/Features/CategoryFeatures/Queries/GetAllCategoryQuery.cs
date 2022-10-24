using Application.Interfaces;
using Domain.Models.Entities;
using MediatR;
using Shared.Wrapper;

namespace Application.Features.CategoryFeatures.Queries
{
    public class GetAllCategoryQuery : IRequest<Result<IEnumerable<Category>>>
    {
    }

    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoryQuery, Result<IEnumerable<Category>>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Result<IEnumerable<Category>>> Handle(GetAllCategoryQuery query, CancellationToken cancellationToken)
        {
            var listCategory = _categoryRepository.GetAll();
            return await Result<IEnumerable<Category>>.SuccessAsync(listCategory);
        }
    }
}