using Application.Interfaces;
using Domain.Models.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderFeatures.Commands
{
    public class UpdateQuantityCommand : IRequest<OrderDetail>
    {
        public int orderDetailId { get; set; }
        public int quantity { get; set; }
    }
    public class UpdateQuantityCommandHandler : IRequestHandler<UpdateQuantityCommand, OrderDetail>
    {
        private readonly IOrderDetailRepository _context;
        public UpdateQuantityCommandHandler( IOrderDetailRepository orderDetailRepository)
        {
            _context =orderDetailRepository;
        }
        public async Task<OrderDetail> Handle(UpdateQuantityCommand command, CancellationToken cancellationToken)
        {
            var orderDetail = await _context.UpdateQuantity(command.orderDetailId, command.quantity);
            return orderDetail;
        }
    }
}
