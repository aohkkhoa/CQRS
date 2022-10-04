using Application.Interfaces;
using Domain.Models.DTO;
using MediatR;

namespace Application.Features.OrderFeatures.Queries
{
    public class GetOrderWillPayQuery : IRequest<List<OrderInformation>>
    {
        public string CustomerName { get; set; }
    }
    public class GetOrderWillPayQueryHandler : IRequestHandler<GetOrderWillPayQuery, List<OrderInformation>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderWillPayQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository= orderRepository;
        }

        public async Task<List<OrderInformation>> Handle(GetOrderWillPayQuery query, CancellationToken cancellationToken)
        {
            var productList = await _orderRepository.getAllOrderWillPay(query.CustomerName);
            return productList;

        }
    }
}
