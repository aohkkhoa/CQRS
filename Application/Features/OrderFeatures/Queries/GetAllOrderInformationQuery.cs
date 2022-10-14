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
        GetAllOrderInformationQueryHandler : IRequestHandler<GetAllOrderInformationQuery,
            Result<IEnumerable<OrderInformation>>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetAllOrderInformationQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result<IEnumerable<OrderInformation>>> Handle(GetAllOrderInformationQuery query,
            CancellationToken cancellationToken)
        {
            var productList = await _orderRepository.getAllOrderInformation(query.CustomerId);
            return await Result<IEnumerable<OrderInformation>>.SuccessAsync(productList, "Ok");
        }
    }
}