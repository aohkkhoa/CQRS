using Application.Interfaces;
using Domain.Models.Entities;
using Persistence.Context;
using Application.Exceptions;
using Persistence.Repositories;

namespace Persistence.repositories
{
    public class OrderDetailRepository : GenericRepository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}