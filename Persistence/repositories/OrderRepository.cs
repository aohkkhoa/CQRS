using Application.Interfaces;
using Domain.Models.DTO;
using Domain.Models.Entities;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Persistence.repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context=context;
        }

        public async Task<int> AddOrder(Order order)
        {
             _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order.OrderId;
        }

        public Task<List<OrderInformation>> getAllOrderInformation(string cusName)
        {
            var order =  (from o in _context.Orders
                         join b in _context.Books on o.BookId equals b.Id
                         join c in _context.Customers on o.CustomerId equals c.CustomerId
                         join od in _context.OrderDetails on o.OrderId equals od.OrderId
                         where c.CustomerName == cusName 
                         select new OrderInformation()
                         {
                             OrderDetailId = od.OrderId,
                             BookName = b.Title,
                             CustomerName = c.CustomerName,
                             Price = b.Price,
                             Quantity = od.Quantity,
                             UnitPrice = od.UnitPrice,
                             CheckPaid = od.CheckPaid
                         }).ToList();
            return Task.FromResult(order);
        }

        public Task<List<OrderInformation>> getAllOrderWillPay(string cusName)
        {
            var order = (from o in _context.Orders
                         join b in _context.Books on o.BookId equals b.Id
                         join c in _context.Customers on o.CustomerId equals c.CustomerId
                         join od in _context.OrderDetails on o.OrderId equals od.OrderId
                         where c.CustomerName == cusName && od.CheckPaid == 0
                         select new OrderInformation()
                         {
                             OrderDetailId = od.OrderDetailId,
                             BookName = b.Title,
                             CustomerName = c.CustomerName,
                             Price = b.Price,
                             Quantity = od.Quantity,
                             UnitPrice = od.UnitPrice,
                             CheckPaid = od.CheckPaid
                         }).ToList();
            return Task.FromResult(order);
        }
        public float getTotalPrice(List<OrderInformation> orderInformation)
        {
            float totalPrice = 0;
            foreach (var order in orderInformation)
            {
                totalPrice = totalPrice + order.UnitPrice;
            }
            return totalPrice;
        }
        public float getPriceByBookId(int bookId)
        {
            var a = (from b in _context.Books
                    where b.Id == bookId
                    select b.Price);
            
           // Console.WriteLine(a.First());
            return a.First();
        }

        public async Task<int> paid(List<OrderInformation> orderInformation)
        {
            foreach (var orderDetails in from item in orderInformation
                                         let order = _context.OrderDetails.Where(a => a.OrderDetailId == item.OrderDetailId).FirstOrDefault()
                                         select order)
            {
                orderDetails.CheckPaid = 1;
                await _context.SaveChangesAsync();
            }

            return 200;
        }
    }
}
