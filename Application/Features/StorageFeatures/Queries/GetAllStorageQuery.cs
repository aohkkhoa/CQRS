using Application.Interfaces;
using Domain.Models.DTO;
using MediatR;
using Shared.Wrapper;

namespace Application.Features.StorageFeatures.Queries
{
    public class GetAllStorageQuery : IRequest<Result<IEnumerable<StorageUnit>>>
    {
    }

    public class GetAllStorageQueryHandler : IRequestHandler<GetAllStorageQuery, Result<IEnumerable<StorageUnit>>>
    {
        private readonly IStorageRepository _storageRepository;
        private readonly IBookRepository _bookRepository;

        public GetAllStorageQueryHandler(IStorageRepository storageRepository, IBookRepository bookRepository)
        {
            _storageRepository = storageRepository;
            _bookRepository = bookRepository;
        }

        public Task<Result<IEnumerable<StorageUnit>>> Handle(GetAllStorageQuery query,
            CancellationToken cancellationToken)
        {
            var listStorage = from s in _storageRepository.Entities
                              join b in _bookRepository.Entities on s.BookId equals b.Id
                              select new StorageUnit()
                              {
                                  BookName = b.Title,
                                  Quantity = s.Quantity,
                                  StorageUnitId = s.StorageId
                              };
            return Result<IEnumerable<StorageUnit>>.SuccessAsync(listStorage);
        }
    }
}