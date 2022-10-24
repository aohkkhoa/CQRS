using Application.Interfaces;
using Domain.Models.DTO;
using Domain.Models.Entities;
using MediatR;
using Shared.Wrapper;

namespace Application.Features.OrderFeatures.Commands
{
    public class CreateOrderCommand : IRequest<IResult>
    {
        public int CustomerId { get; set; }
        public List<OrderCreate> ListOrderCreate { get; set; }

        public class CreateBookCommandHandler : IRequestHandler<CreateOrderCommand, IResult>
        {
            private readonly IOrderRepository _orderRepository;
            private readonly IOrderDetailRepository _orderDetailRepository;
            private readonly IBookRepository _bookRepository;

            public CreateBookCommandHandler(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, IBookRepository bookRepository)
            {
                _orderRepository = orderRepository;
                _orderDetailRepository = orderDetailRepository;
                _bookRepository = bookRepository;
            }

            public async Task<IResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
            {
                foreach (var item in command.ListOrderCreate)
                {
                    var order = new Order
                    {
                        CustomerId = item.CustomerId,
                        BookId = item.BookId
                    };
                    var orderResult = await _orderRepository.AddOrder(order, item.Quantity);
                    var price = (from b in _bookRepository.Entities
                                 where b.Id == item.BookId
                                 select b.Price).First();
                    if (orderResult.Succeeded)
                    {
                        var orderDetail = new OrderDetail
                        {
                            OrderId = orderResult.Data.OrderId,
                            Quantity = item.Quantity,
                            UnitPrice = price * item.Quantity,
                            CheckPaid = 0
                        };
                        _orderDetailRepository.Insert(orderDetail);
                        await _orderDetailRepository.Save();
                    }
                    else return await Result.FailAsync(orderResult.Messages);
                }

                return await Result.SuccessAsync("Add Success !");
            }
        }
    }
}