using Application.Exceptions;
using Application.Interfaces;
using MediatR;
using Shared.Wrapper;

namespace Application.Features.OrderFeatures.Commands
{
    public class DeleteOrderDetailCommand : IRequest<IResult>
    {
        public int OrderDetailId { get; set; }
    }

    public class DeleteOrderDetailCommandHandler : IRequestHandler<DeleteOrderDetailCommand, IResult>
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IOrderRepository _orderRepository;

        public DeleteOrderDetailCommandHandler(IOrderDetailRepository orderDetailRepository, IOrderRepository orderRepository)
        {
            _orderDetailRepository = orderDetailRepository;
            _orderRepository = orderRepository;
        }

        public async Task<IResult> Handle(DeleteOrderDetailCommand command, CancellationToken cancellationToken)
        {
            var orderDetailsByOrderDetailId = _orderDetailRepository.Entities.FirstOrDefault(od => od.OrderDetailId == command.OrderDetailId);
            if (orderDetailsByOrderDetailId is null)
                throw new ApiException("OrderDetail Not Found !");
            _orderDetailRepository.Delete(command.OrderDetailId);
            await _orderDetailRepository.Save();
            var orderByOrderDetail = _orderRepository.Entities.FirstOrDefault(o => o.OrderId == orderDetailsByOrderDetailId.OrderId);
            if (orderByOrderDetail is null)
                throw new ApiException("Order Not Found !");
            _orderRepository.Delete(orderByOrderDetail.OrderId);
            await _orderRepository.Save();
            return await Result.SuccessAsync("Delete Complete !");
        }
    }
}