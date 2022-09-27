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
    public class GetTotalPriceWillPayQuery : IRequest<float>
    {
        public string CustomerName { get; set; }
    }
    public class GetTotalPriceWillPayQueryHandler : IRequestHandler<GetTotalPriceWillPayQuery, float>
    {
        private readonly IOrderRepository _context;

        public GetTotalPriceWillPayQueryHandler(IOrderRepository context)
        {
            _context=context;
        }

        public async Task<float> Handle(GetTotalPriceWillPayQuery query, CancellationToken cancellationToken)
        {
            var productList = await _context.getAllOrderWillPay(query.CustomerName);
            var totalPrice = _context.getTotalPrice(productList);
            return totalPrice;
           
        }
    }
}
