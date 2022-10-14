using Application.Interfaces;
using MediatR;
using Shared.Wrapper;

namespace Application.Features.OrderFeatures.Queries
{
    public class GetTotalPriceWillPayQuery : IRequest<IResult<float>>
    {
        public int CustomerId { get; set; }
    }

    public class GetTotalPriceWillPayQueryHandler : IRequestHandler<GetTotalPriceWillPayQuery, IResult<float>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetTotalPriceWillPayQueryHandler(IOrderRepository context)
        {
            _orderRepository = context;
        }

        public async Task<IResult<float>> Handle(GetTotalPriceWillPayQuery query, CancellationToken cancellationToken)
        {
            var productList = await _orderRepository.getAllOrderWillPay(query.CustomerId);
            var totalPrice = _orderRepository.getTotalPrice(productList);
            return await Result<float>.SuccessAsync(totalPrice, "OK");
        }
    }
}