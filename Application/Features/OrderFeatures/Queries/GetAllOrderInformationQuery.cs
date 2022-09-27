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
    public class GetAllOrderInformationQuery : IRequest<IEnumerable<OrderInformation>>
    {
        public string CustomerName { get; set; }
    }
    public class GetAllOrderInformationQueryHandler : IRequestHandler<GetAllOrderInformationQuery, IEnumerable<OrderInformation>>
    {
        private readonly IOrderRepository _context;

        public GetAllOrderInformationQueryHandler(IOrderRepository context)
        {
            _context=context;
        }

        public async Task<IEnumerable<OrderInformation>> Handle(GetAllOrderInformationQuery query, CancellationToken cancellationToken)
        {
            var productList = await _context.getAllOrderInformation(query.CustomerName);
            if (productList == null)
            {
                return null;
            }
            return productList.ToList();
           
        }
    }
}
