using Application.Features.BookFeatures.Commands;
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
    public class CreateOrderCommand : IRequest<int>
    {
        public int BookId { get; set; }
        public int CustomerId { get; set; }
        public int Quantity { get; set; }
        public class CreateBookCommandHandler : IRequestHandler<CreateOrderCommand, int>
        {
            private readonly IOrderRepository _context;
            private readonly IOrderDetailRepository _orderDetailRepository;
            public CreateBookCommandHandler(IOrderRepository context, IOrderDetailRepository orderDetailRepository)
            {
                _context = context;
                _orderDetailRepository = orderDetailRepository;
            }
            public async Task<int> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
            {
                var order = new Order();
                order.CustomerId = command.CustomerId;
                order.BookId = command.BookId;
                var orderId= await _context.AddOrder(order);
                var price = _context.getPriceByBookId(command.BookId);
                var orderDetail = new OrderDetail();
                orderDetail.OrderId =orderId;
                orderDetail.Quantity =command.Quantity;
                orderDetail.UnitPrice =price*command.Quantity;
                orderDetail.CheckPaid = 0;
                var orderDetailId = await _orderDetailRepository.AddOrderDetail(orderDetail);
                return orderDetailId;
            }
        }
    }
}
