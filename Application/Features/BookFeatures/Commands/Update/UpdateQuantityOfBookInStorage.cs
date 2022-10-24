using Application.Exceptions;
using Application.Interfaces;
using MediatR;
using Shared.Wrapper;

namespace Application.Features.BookFeatures.Commands.Update
{
    public class UpdateQuantityOfBookInStorage : IRequest<IResult>
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
    }

    public class UpdateQuantityOfBookInStorageHandle : IRequestHandler<UpdateQuantityOfBookInStorage, IResult>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IStorageRepository _storageRepository;

        public UpdateQuantityOfBookInStorageHandle(IBookRepository bookRepository, IStorageRepository storageRepository)
        {
            _storageRepository = storageRepository;
            _bookRepository = bookRepository;
        }

        public async Task<IResult> Handle(UpdateQuantityOfBookInStorage request, CancellationToken cancellationToken)
        {
            var book = _bookRepository.Entities.FirstOrDefault(b => b.Id == request.BookId);
            if (book is null)
                throw new ApiException("Book Not Found !");
            var storage = _storageRepository.Entities.FirstOrDefault(s => s.BookId == request.BookId);
            if (storage is null)
                throw new ApiException("Storage Not Found !");
            storage.Quantity += request.Quantity;
            await _storageRepository.Save();
            return await Result.SuccessAsync("Update Quantity Complete !");
        }
    }
}