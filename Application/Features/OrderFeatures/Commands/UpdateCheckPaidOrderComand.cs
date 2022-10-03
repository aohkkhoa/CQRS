using Application.Interfaces;
using Domain.Models.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderFeatures.Commands
{
    public class UpdateCheckPaidOrderComand : IRequest<int>
    {
        public string CustomerName { get; set; }
        public class updateCheckPaidOrderComandHandler : IRequestHandler<UpdateCheckPaidOrderComand, int>
        {
            private readonly IOrderRepository _context;
            public updateCheckPaidOrderComandHandler(IOrderRepository context, IOrderDetailRepository orderDetailRepository)
            {
                _context = context;
            }
            public async Task<int> Handle(UpdateCheckPaidOrderComand command, CancellationToken cancellationToken)
            {
                var productList = await _context.getAllOrderWillPay(command.CustomerName);
                var result =await _context.paid(productList);
                return 200;
            }
        }
    }
}
