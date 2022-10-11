using Application.Interfaces;
using Domain.Models.Entities;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Persistence.repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderDetailRepository(ApplicationDbContext context)
        {
            _context=context;
        }
        public async Task<int> AddOrderDetail(OrderDetail orderDetail)
        {
            _context.OrderDetails.Add(orderDetail);
            await _context.SaveChangesAsync();
            return orderDetail.OrderDetailId;
        }

        public async Task<int> DeleteOrderDetailById(int orderDeteailId)
        {
            var item =  _context.OrderDetails.FirstOrDefault(a => a.OrderDetailId == orderDeteailId);
            if (item == null) return default;
            _context.OrderDetails.Remove(item);
            await _context.SaveChangesAsync();
            var order =  _context.Orders.FirstOrDefault(a => a.OrderId == item.OrderId);
            if (order == null) return default;
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return item.OrderDetailId;
        }

        public async Task<OrderDetail> UpdateQuantity(int orderDetailId, int quantity)
        {

            var orderDetail =  _context.OrderDetails.FirstOrDefault(a => a.OrderDetailId == orderDetailId);
            if (orderDetail == null)
            {
                return null;
            }
            else
            {
                var order = _context.Orders.FirstOrDefault(a => a.OrderId == orderDetail.OrderId);

                var book = _context.Books.FirstOrDefault(a => a.Id == order.BookId);
                
                orderDetail.Quantity = quantity;
                orderDetail.UnitPrice = quantity * book.Price;
                await _context.SaveChangesAsync();
                return orderDetail;
            }
        }
    }
}
