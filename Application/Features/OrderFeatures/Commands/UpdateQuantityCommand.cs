using Application.Interfaces;
using Domain.Models.Entities;
using MediatR;
using Shared.Wrapper;

namespace Application.Features.OrderFeatures.Commands
{
    public class UpdateQuantityCommand : IRequest<Result<OrderDetail>>
    {
        public int orderDetailId { get; set; }
        public int quantity { get; set; }
    }

    public class UpdateQuantityCommandHandler : IRequestHandler<UpdateQuantityCommand, Result<OrderDetail>>
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public UpdateQuantityCommandHandler(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<Result<OrderDetail>> Handle(UpdateQuantityCommand command,
            CancellationToken cancellationToken)
        {
            var orderDetail = await _orderDetailRepository.UpdateQuantity(command.orderDetailId, command.quantity);
            return await Result<OrderDetail>.SuccessAsync(orderDetail, "Update Quantity Complete!");
        }
    }
}