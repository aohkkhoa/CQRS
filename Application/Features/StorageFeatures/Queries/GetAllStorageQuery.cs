using Application.Interfaces;
using Domain.Models.DTO;
using MediatR;

namespace Application.Features.StorageFeatures.Queries
{
    public class GetAllStorageQuery : IRequest<IEnumerable<StorageUnit>>
    {
    }
    public class GetAllStorageQueryHandler : IRequestHandler<GetAllStorageQuery, IEnumerable<StorageUnit>>
    {
        private readonly IStorageRepository _storageRepository;
        public GetAllStorageQueryHandler(IStorageRepository storageRepository)
        {
            _storageRepository = storageRepository;
        }
        public Task<IEnumerable<StorageUnit>> Handle(GetAllStorageQuery query, CancellationToken cancellationToken)
        {
            var storage = _storageRepository.GetAllStorage();
            return Task.FromResult(storage);
        }
    }
}
