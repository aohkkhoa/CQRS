using Application.Interfaces;
using Domain.Models.DTO;
using MediatR;
using Shared.Wrapper;

namespace Application.Features.OrderFeatures.Queries
{
    public class GetAllOrderInformationQuery : IRequest<Result<IEnumerable<OrderInformation>>>
    {
        public int CustomerId { get; set; }
    }

    public class
        GetAllOrderInformationQueryHandler : IRequestHandler<GetAllOrderInformationQuery, Result<IEnumerable<OrderInformation>>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IOrderRepository _orderRepository;

        public GetAllOrderInformationQueryHandler(IOrderRepository orderRepository, IBookRepository bookRepository,
            ICustomerRepository customerRepository, IOrderDetailRepository orderDetailRepository)
        {
            _bookRepository = bookRepository;
            _customerRepository = customerRepository;
            _orderDetailRepository = orderDetailRepository;
            _orderRepository = orderRepository;
        }

        public async Task<Result<IEnumerable<OrderInformation>>> Handle(GetAllOrderInformationQuery query, CancellationToken cancellationToken)
        {
            var listOrderInformation = from o in _orderRepository.Entities
                                       join b in _bookRepository.Entities on o.BookId equals b.Id
                                       join c in _customerRepository.Entities on o.CustomerId equals c.CustomerId
                                       join od in _orderDetailRepository.Entities on o.OrderId equals od.OrderId
                                       where c.CustomerId == query.CustomerId
                                       orderby od.OrderDetailId descending
                                       select new OrderInformation()
                                       {
                                           OrderDetailId = od.OrderId,
                                           BookName = b.Title,
                                           BookId = b.Id,
                                           CustomerName = c.CustomerName,
                                           Price = b.Price,
                                           Quantity = od.Quantity,
                                           UnitPrice = od.UnitPrice,
                                           CheckPaid = od.CheckPaid
                                       };

            return await Result<IEnumerable<OrderInformation>>.SuccessAsync(listOrderInformation);
        }
    }
}