using Application.Interfaces;
using Domain.Models.DTO;
using MediatR;
using Shared.Wrapper;

namespace Application.Features.OrderFeatures.Queries
{
    public class GetOrderWillPayQuery : IRequest<IResult<List<OrderInformation>>>
    {
        public int CustomerId { get; set; }
    }

    public class GetOrderWillPayQueryHandler : IRequestHandler<GetOrderWillPayQuery, IResult<List<OrderInformation>>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderWillPayQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IResult<List<OrderInformation>>> Handle(GetOrderWillPayQuery query,
            CancellationToken cancellationToken)
        {
            var productList = await _orderRepository.getAllOrderWillPay(query.CustomerId);
            return await Result<List<OrderInformation>>.SuccessAsync(productList, "Ok");
        }
    }
}