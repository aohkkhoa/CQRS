using Application.Interfaces;
using MediatR;

namespace Application.Features.OrderFeatures.Commands
{
    public class DeleteOrderDetailCommand : IRequest<int>
    {
        public int OrderDeteailId { get; set; }
    }
    public class DeleteOrderDetailCommandHandler : IRequestHandler<DeleteOrderDetailCommand, int>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        public DeleteOrderDetailCommandHandler(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
        }
        public async Task<int> Handle(DeleteOrderDetailCommand command, CancellationToken cancellationToken)
        {
            var orderDetailId = await _orderDetailRepository.DeleteOrderDetailById(command.OrderDeteailId);
            return 200;
        }
    }
}
