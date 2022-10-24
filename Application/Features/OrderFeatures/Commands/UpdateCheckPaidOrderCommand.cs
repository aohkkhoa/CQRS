using Application.Exceptions;
using Application.Interfaces;
using MediatR;
using Shared.Wrapper;

namespace Application.Features.OrderFeatures.Commands
{
    public class UpdateCheckPaidOrderCommand : IRequest<IResult>
    {
        public int CustomerId { get; set; }
    }

    public class UpdateCheckPaidOrderCommandHandler : IRequestHandler<UpdateCheckPaidOrderCommand, IResult>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IStorageRepository _storageRepository;

        public UpdateCheckPaidOrderCommandHandler(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, IBookRepository bookRepository, IStorageRepository storageRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _bookRepository = bookRepository;
            _storageRepository = storageRepository;
        }

        public async Task<IResult> Handle(UpdateCheckPaidOrderCommand command, CancellationToken cancellationToken)
        {
            var orderWillPay = await _orderRepository.GetAllOrderWillPay(command.CustomerId);
            var orderDetailWillPay = (from item in orderWillPay
                                      let orderDetail = _orderDetailRepository.Entities.FirstOrDefault(a => a.OrderDetailId == item.OrderDetailId)
                                      select orderDetail);
            if (orderDetailWillPay is null)
                throw new ApiException("OrderDetail Not Found !");
            foreach (var orderDetails in orderDetailWillPay)
            {
                orderDetails.CheckPaid = 1;
                var order = _orderRepository.Entities.FirstOrDefault(b => b.OrderId == orderDetails.OrderId);
                if (order is null)
                    throw new ApiException("Order Not Found !");
                var book = _bookRepository.Entities.FirstOrDefault(c => c.Id == order.BookId);
                if (book is null)
                    throw new ApiException("Book Not Found !");
                var storage = _storageRepository.Entities.FirstOrDefault(s => s.BookId == book.Id);
                if (storage is null)
                    throw new ApiException("Storage Not Found !");
                storage.Quantity -= orderDetails.Quantity;
            }

            await _orderDetailRepository.Save();
            await _storageRepository.Save();
            return await Result.SuccessAsync("Paid Complete");
        }
    }
}