using Application.Exceptions;
using Application.Interfaces;
using Domain.Models.Entities;
using MediatR;
using Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.BookFeatures.Commands.Delete
{
    public class DeleteBookCommand : IRequest<Result<Book>>
    {
        public int BookId { get; set; }
    }

    public class DeleteBookCommandHandle : IRequestHandler<DeleteBookCommand, Result<Book>>
    {
        private readonly IBookRepository _bookRepository;

        public DeleteBookCommandHandle(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Result<Book>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var bookDelete = _bookRepository.Entities.FirstOrDefault(bd => bd.Id == request.BookId);
            if (bookDelete is null) throw new ApiException("Book Not Found !");
            bookDelete.IsDeleted = true;
            await _bookRepository.Save();
            return await Result<Book>.SuccessAsync("Delete Logic Complete !");
        }
    }
}