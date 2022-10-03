using Application.Features.BookFeatures.Commands;
using Application.Interfaces;
using Domain.Models.DTO;
using Domain.Models.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderFeatures.Commands
{
    public class CreateOrderCommand : IRequest<string>
    {
        /*public int BookId { get; set; }
        public int CustomerId { get; set; }
        public int Quantity { get; set; }*/
        public List<OrderCreate> OrderCreate { get; set; }
        public class CreateBookCommandHandler : IRequestHandler<CreateOrderCommand, string>
        {
            private readonly IOrderRepository _context;
            private readonly IOrderDetailRepository _orderDetailRepository;
            public CreateBookCommandHandler(IOrderRepository context, IOrderDetailRepository orderDetailRepository)
            {
                _context = context;
                _orderDetailRepository = orderDetailRepository;
            }
            public async Task<string> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
            {

                try
                {
                    var test = await _context.getAllOrderWillPay("customerA");
                    foreach (var item in command.OrderCreate)
                    {
                        List<OrderCreate> a = new List<OrderCreate>() {new OrderCreate()
                                                                       {BookId=item.BookId,
                                                                        CustomerId=item.CustomerId,
                                                                        Quantity = item.Quantity,
                                                                        }};
                        if (test.Count()==0) a = command.OrderCreate;
                        var count1 = 0;
                        foreach (var item2 in test)
                        {
                            if (item.BookId==item2.BookId)
                            {
                                count1++;
                                var orderDetailTest = _context.findOrderDetailByBookId(item.BookId);
                                if (orderDetailTest.Count()!=0)
                                    await _context.addQuantityOfOrderDetail(orderDetailTest, item.Quantity);
                            }
                        }
                        if (count1==0)
                        {
                            foreach (var item5 in a)
                            {
                                var order = new Order();
                                order.CustomerId = item5.CustomerId;
                                order.BookId = item5.BookId;
                                var orderId = await _context.AddOrder(order, item5.Quantity);
                                var price = _context.getPriceByBookId(item5.BookId);
                                if (orderId !=-1)
                                {
                                    var orderDetail = new OrderDetail();
                                    orderDetail.OrderId =orderId;
                                    orderDetail.Quantity =item5.Quantity;
                                    orderDetail.UnitPrice =price*item5.Quantity;
                                    orderDetail.CheckPaid = 0;
                                    var orderDetailId = await _orderDetailRepository.AddOrderDetail(orderDetail);
                                }
                                else return "maybe the quantity is not enough";
                            }
                            return "add compelete";
                        }
                    }
                }
                catch (Exception ex) { return "maybe the quantity is not enough  "; }
                return "finish";
                //_storageRepository.HandleQuantityStorage(order.BookId, command.Quantity);
            }
        }
    }
}
