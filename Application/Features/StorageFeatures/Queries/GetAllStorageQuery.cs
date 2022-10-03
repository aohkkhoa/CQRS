using Application.Interfaces;
using Domain.Models.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.StorageFeatures.Queries
{
    public class GetAllStorageQuery : IRequest<IEnumerable<StorageUnit>>
    {

    }
    public class GetAllStorageQueryHandler : IRequestHandler<GetAllStorageQuery, IEnumerable<StorageUnit>>
    {
        private readonly IStorageRepository _RS;
            public GetAllStorageQueryHandler(IStorageRepository storageRepository)
        {
            _RS = storageRepository;
        }
        public async Task<IEnumerable<StorageUnit>> Handle(GetAllStorageQuery query, CancellationToken cancellationToken)
        {
            var storage = _RS.GetAllStorage();
            return storage;
        }
    }
}
