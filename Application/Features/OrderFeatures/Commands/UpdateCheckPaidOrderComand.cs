using Application.Interfaces;
using MediatR;

namespace Application.Features.OrderFeatures.Commands
{
    public class UpdateCheckPaidOrderComand : IRequest<int>
    {
        public string CustomerName { get; set; }
        public class updateCheckPaidOrderComandHandler : IRequestHandler<UpdateCheckPaidOrderComand, int>
        {
            private readonly IOrderRepository _orderRepository;
            public updateCheckPaidOrderComandHandler(IOrderRepository orderRepository)
            {
                _orderRepository = orderRepository;
            }
            public async Task<int> Handle(UpdateCheckPaidOrderComand command, CancellationToken cancellationToken)
            {
                var productList = await _orderRepository.getAllOrderWillPay(command.CustomerName);
                var result = await _orderRepository.paid(productList);
                return 200;
            }
        }
    }
}
