using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderFeatures.Commands
{
    public class DeleteOrderDetailCommand : IRequest<int>
    {
        public int OrderDeteailId { get; set; }
    }
    public class DeleteOrderDetailCommandHandler : IRequestHandler<DeleteOrderDetailCommand, int>
    {
        private readonly IOrderRepository _context;
        private readonly IOrderDetailRepository _orderRepository;
        public DeleteOrderDetailCommandHandler(IOrderRepository context, IOrderDetailRepository orderDetailRepository)
        {
            _context = context;
            _orderRepository = orderDetailRepository;
        }
        public async Task<int> Handle(DeleteOrderDetailCommand command, CancellationToken cancellationToken)
        {
            var orderDetailId = await _orderRepository.DeleteOrderDetailById(command.OrderDeteailId);
            return 200;
        }
    }
}
