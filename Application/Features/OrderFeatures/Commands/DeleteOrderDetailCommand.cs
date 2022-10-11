using Application.Interfaces;
using MediatR;
using Shared.Wrapper;

namespace Application.Features.OrderFeatures.Commands
{
    public class DeleteOrderDetailCommand : IRequest<IResult>
    {
        public int OrderDetailId { get; set; }
    }

    public class DeleteOrderDetailCommandHandler : IRequestHandler<DeleteOrderDetailCommand, IResult>
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public DeleteOrderDetailCommandHandler(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<IResult> Handle(DeleteOrderDetailCommand command, CancellationToken cancellationToken)
        {
            await _orderDetailRepository.DeleteOrderDetailById(command.OrderDetailId);
            return await Result.SuccessAsync("Delete Complete");
        }
    }
}