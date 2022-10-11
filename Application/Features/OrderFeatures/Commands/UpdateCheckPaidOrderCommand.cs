using Application.Interfaces;
using MediatR;
using Shared.Wrapper;

namespace Application.Features.OrderFeatures.Commands
{
    public class UpdateCheckPaidOrderCommand : IRequest<IResult>
    {
        public int CustomerId { get; set; }
    }

    public class UpdateCheckPaidOrderCommandHandler : IRequestHandler<UpdateCheckPaidOrderCommand, IResult>
    {
        private readonly IOrderRepository _orderRepository;

        public UpdateCheckPaidOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IResult> Handle(UpdateCheckPaidOrderCommand command, CancellationToken cancellationToken)
        {
            var productList = await _orderRepository.getAllOrderWillPay(command.CustomerId);
            await _orderRepository.paid(productList);
            return await Result.SuccessAsync("Paid Complete");
        }
    }
}