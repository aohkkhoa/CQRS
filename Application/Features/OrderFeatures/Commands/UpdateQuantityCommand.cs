using Application.Interfaces;
using Domain.Models.Entities;
using MediatR;

namespace Application.Features.OrderFeatures.Commands
{
    public class UpdateQuantityCommand : IRequest<OrderDetail>
    {
        public int orderDetailId { get; set; }
        public int quantity { get; set; }
    }
    public class UpdateQuantityCommandHandler : IRequestHandler<UpdateQuantityCommand, OrderDetail>
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        public UpdateQuantityCommandHandler(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository =orderDetailRepository;
        }
        public async Task<OrderDetail> Handle(UpdateQuantityCommand command, CancellationToken cancellationToken)
        {
            var orderDetail = await _orderDetailRepository.UpdateQuantity(command.orderDetailId, command.quantity);
            return orderDetail;
        }
    }
}
