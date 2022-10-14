using Application.Interfaces;
using MediatR;
using Shared.Wrapper;

namespace Application.Features.StorageFeatures.Commands
{
    public class DeleteCategoryCommand : IRequest<IResult>
    {
        public int CategoryId { get; set; }
    }

    public class DelegateCategoryCommandHandle : IRequestHandler<DeleteCategoryCommand, IResult>
    {
        private readonly ICategoryRepository _categoryRepository;

        public DelegateCategoryCommandHandle(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Task<IResult> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var result = _categoryRepository.DeleteCategory(request.CategoryId);
            if (!result.Result.Succeeded && result.Result.Messages.Count == 1)
            {
                return Result.FailAsync("This category has many book");
            }

            return Result.SuccessAsync("DeleteOk");
        }
    }
}