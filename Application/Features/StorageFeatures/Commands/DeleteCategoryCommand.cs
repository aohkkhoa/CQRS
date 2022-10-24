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
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;

        public DelegateCategoryCommandHandle(ICategoryRepository categoryRepository, IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IResult> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var books = _bookRepository.GetById(request.CategoryId);
            if (books is not null)
            {
                return await Result.FailAsync("This category has many book !");
            }

            var category = _categoryRepository.GetById(request.CategoryId);
            if (category is null)
            {
                return await Result.FailAsync("This category not found !");
            }
            _categoryRepository.Delete(request.CategoryId);
            await _categoryRepository.Save();
            return await Result.SuccessAsync("Delete Complete !");
        }
    }
}