using Application.Interfaces;
using Domain.Models.DTO;
using Domain.Models.Entities;
using MediatR;
using Shared.Wrapper;

namespace Application.Features.OrderFeatures.Commands
{
    public class CreateOrderCommand : IRequest<IResult>
    {
        public int customerId { get; set; }
        public List<OrderCreate> OrderCreate { get; set; }


        public class CreateBookCommandHandler : IRequestHandler<CreateOrderCommand, IResult>
        {
            private readonly IOrderRepository _orderRepository;
            private readonly IOrderDetailRepository _orderDetailRepository;

            public CreateBookCommandHandler(IOrderRepository context, IOrderDetailRepository orderDetailRepository)
            {
                _orderRepository = context;
                _orderDetailRepository = orderDetailRepository;
            }

            public async Task<IResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var checkAnyOrder = await _orderRepository.getAllOrderWillPay(1);
                    foreach (var item in command.OrderCreate)
                    {
                        List<OrderCreate> orderCreate = new List<OrderCreate>()
                        {
                            new()
                            {
                                BookId = item.BookId,
                                CustomerId = item.CustomerId,
                                Quantity = item.Quantity,
                            }
                        };
                        if (!checkAnyOrder.Any()) orderCreate = command.OrderCreate;
                        var count = 0;
                        foreach (var item2 in checkAnyOrder)
                        {
                            if (item.BookId == item2.BookId)
                            {
                                count++;
                                var orderDetailTest = _orderRepository.findOrderDetailByBookId(item.BookId);
                                if (orderDetailTest.Count != 0)
                                    await _orderRepository.addQuantityOfOrderDetail(orderDetailTest, item.Quantity);
                            }
                        }

                        if (count == 0)
                        {
                            foreach (var item2 in orderCreate)
                            {
                                var order = new Order
                                {
                                    CustomerId = item2.CustomerId,
                                    BookId = item2.BookId
                                };
                                var orderResult = await _orderRepository.AddOrder(order, item2.Quantity);
                                var price = _orderRepository.getPriceByBookId(item2.BookId);
                                if (orderResult.Succeeded)
                                {
                                    var orderDetail = new OrderDetail
                                    {
                                        OrderId = orderResult.Data.OrderId,
                                        Quantity = item2.Quantity,
                                        UnitPrice = price * item2.Quantity,
                                        CheckPaid = 0
                                    };
                                    await _orderDetailRepository.AddOrderDetail(orderDetail);
                                }
                                else return await Result.FailAsync(orderResult.Messages);
                            }

                            return await Result.SuccessAsync("Add Complete!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    return await Result.FailAsync("Maybe the quantity is not enough " + ex);
                }

                return await Result.SuccessAsync("Finish");
                //_storageRepository.HandleQuantityStorage(order.BookId, command.Quantity);
            }
        }
    }
}