using Application.Interfaces;
using Domain.Models.DTO;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Repositories;
using Shared.Wrapper;

namespace Persistence.repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Result<Order>> AddOrder(Order order, int quantity)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                var storage = _context.Storages.FirstOrDefault(a => a.BookId == order.BookId);
                if (storage is null)
                {
                    throw new ApplicationException("Storage Not Found !");
                }

                if (storage.Quantity < quantity)
                {
                    throw new Exception();
                }

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine("Error" + ex);
                return await Result<Order>.FailAsync("Quantity is not enough");
            }

            return await Result<Order>.SuccessAsync(order);
        }

        public async Task<IEnumerable<OrderInformation>> GetAllOrderWillPay(int customerId)
        {
            var order = await (from o in _context.Orders
                               join b in _context.Books on o.BookId equals b.Id
                               join c in _context.Customers on o.CustomerId equals c.CustomerId
                               join od in _context.OrderDetails on o.OrderId equals od.OrderId
                               where c.CustomerId == customerId && od.CheckPaid == 0
                               select new OrderInformation()
                               {
                                   OrderDetailId = od.OrderDetailId,
                                   BookId = b.Id,
                                   BookName = b.Title,
                                   CustomerName = c.CustomerName,
                                   Price = b.Price,
                                   Quantity = od.Quantity,
                                   UnitPrice = od.UnitPrice,
                                   CheckPaid = od.CheckPaid
                               }).ToListAsync();
            return order;
        }
    }
}