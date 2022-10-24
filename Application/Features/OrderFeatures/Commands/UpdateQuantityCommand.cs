using Application.Exceptions;
using Application.Interfaces;
using Domain.Models.Entities;
using MediatR;
using Shared.Wrapper;

namespace Application.Features.OrderFeatures.Commands
{
    public class UpdateQuantityCommand : IRequest<Result<OrderDetail>>
    {
        public int orderDetailId { get; set; }
        public int quantity { get; set; }
    }

    public class UpdateQuantityCommandHandler : IRequestHandler<UpdateQuantityCommand, Result<OrderDetail>>
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IOrderRepository _orderRepository;

        public UpdateQuantityCommandHandler(IOrderDetailRepository orderDetailRepository, IBookRepository bookRepository, IOrderRepository orderRepository)
        {
            _orderDetailRepository = orderDetailRepository;
            _bookRepository = bookRepository;
            _orderRepository = orderRepository;
        }

        public async Task<Result<OrderDetail>> Handle(UpdateQuantityCommand command,
            CancellationToken cancellationToken)
        {
            var orderDetail = _orderDetailRepository.Entities.FirstOrDefault(od => od.OrderDetailId == command.orderDetailId);
            if (orderDetail is null)
                throw new ApiException("OrderDetail Not Found !");
            var order = _orderRepository.Entities.FirstOrDefault(o => o.OrderId == orderDetail.OrderId);
            if (order is null)
                throw new ApiException("Order Not Found !");
            var book = _bookRepository.Entities.FirstOrDefault(b => b.Id == order.BookId);
            if (book is null)
                throw new ApiException("Book Not Found !");
            orderDetail.Quantity = command.quantity;
            orderDetail.UnitPrice = command.quantity * book.Price;
            await _orderDetailRepository.Save();
            return await Result<OrderDetail>.SuccessAsync(orderDetail, "Update Quantity Complete!");
        }
    }
}