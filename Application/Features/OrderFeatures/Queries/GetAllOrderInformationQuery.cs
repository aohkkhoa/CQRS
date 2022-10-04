using Application.Interfaces;
using Domain.Models.DTO;
using MediatR;

namespace Application.Features.OrderFeatures.Queries
{
    public class GetAllOrderInformationQuery : IRequest<IEnumerable<OrderInformation>>
    {
        public string CustomerName { get; set; }
    }
    public class GetAllOrderInformationQueryHandler : IRequestHandler<GetAllOrderInformationQuery, IEnumerable<OrderInformation>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetAllOrderInformationQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository=orderRepository;
        }

        public async Task<IEnumerable<OrderInformation>> Handle(GetAllOrderInformationQuery query, CancellationToken cancellationToken)
        {
            var productList = await _orderRepository.getAllOrderInformation(query.CustomerName);
            if (productList == null)
            {
                return null;
            }
            return productList.ToList();
           
        }
    }
}
