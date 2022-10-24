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
            var listOrderWillPay = await _orderRepository.GetAllOrderWillPay(query.CustomerId);
            var totalPrice = 0.0f;
            foreach (var item in listOrderWillPay)
            {
                totalPrice += item.UnitPrice;
            }
            return await Result<float>.SuccessAsync(totalPrice);
        }
    }
}