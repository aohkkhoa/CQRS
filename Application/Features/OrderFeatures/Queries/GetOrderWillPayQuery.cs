using Application.Interfaces;
using Domain.Models.DTO;
using MediatR;
using Shared.Wrapper;

namespace Application.Features.OrderFeatures.Queries
{
    public class GetOrderWillPayQuery : IRequest<IResult<IEnumerable<OrderInformation>>>
    {
        public int CustomerId { get; set; }
    }

    public class GetOrderWillPayQueryHandler : IRequestHandler<GetOrderWillPayQuery, IResult<IEnumerable<OrderInformation>>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderWillPayQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IResult<IEnumerable<OrderInformation>>> Handle(GetOrderWillPayQuery query, CancellationToken cancellationToken)
        {
            var listOrderWillPay = await _orderRepository.GetAllOrderWillPay(query.CustomerId);

            return await Result<IEnumerable<OrderInformation>>.SuccessAsync(listOrderWillPay);
        }
    }
}