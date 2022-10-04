using Application.Interfaces;
using MediatR;

namespace Application.Features.OrderFeatures.Queries
{
    public class GetTotalPriceWillPayQuery : IRequest<float>
    {
        public string CustomerName { get; set; }
    }
    public class GetTotalPriceWillPayQueryHandler : IRequestHandler<GetTotalPriceWillPayQuery, float>
    {
        private readonly IOrderRepository _orderRepository;

        public GetTotalPriceWillPayQueryHandler(IOrderRepository context)
        {
            _orderRepository=context;
        }

        public async Task<float> Handle(GetTotalPriceWillPayQuery query, CancellationToken cancellationToken)
        {
            var productList = await _orderRepository.getAllOrderWillPay(query.CustomerName);
            var totalPrice = _orderRepository.getTotalPrice(productList);
            return totalPrice;
           
        }
    }
}
