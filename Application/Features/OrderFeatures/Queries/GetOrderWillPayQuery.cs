using Application.Features.CategoryFeatures.Queries;
using Application.Interfaces;
using Domain.Models.DTO;
using Domain.Models.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderFeatures.Queries
{
    public class GetOrderWillPayQuery : IRequest<List<OrderInformation>>
    {
        public string CustomerName { get; set; }
    }
    public class GetOrderWillPayQueryHandler : IRequestHandler<GetOrderWillPayQuery, List<OrderInformation>>
    {
        private readonly IOrderRepository _context;

        public GetOrderWillPayQueryHandler(IOrderRepository context)
        {
            _context=context;
        }

        public async Task<List<OrderInformation>> Handle(GetOrderWillPayQuery query, CancellationToken cancellationToken)
        {
            var productList = await _context.getAllOrderWillPay(query.CustomerName);
            return productList;
           
        }
    }
}
