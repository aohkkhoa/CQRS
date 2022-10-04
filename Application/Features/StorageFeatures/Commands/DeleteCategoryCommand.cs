using Application.Interfaces;
using MediatR;

namespace Application.Features.StorageFeatures.Commands
{
    public class DeleteCategoryCommand : IRequest<string>
    {
        public int categoryId { get; set; }

        public class DelegateCategoryCommandHandle : IRequestHandler<DeleteCategoryCommand, string>
        {
            private readonly ICategoryRepository _categoryRepository;

            public DelegateCategoryCommandHandle(ICategoryRepository categoryRepository)
            {
                _categoryRepository=categoryRepository;
            }
            public Task<string> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
            {
               var result = _categoryRepository.DeleteCategory(request.categoryId);
                if(result == 1)
                {
                    return Task.FromResult("this category has many book");
                }
               return Task.FromResult("haha");
            }
        }
    }
}
