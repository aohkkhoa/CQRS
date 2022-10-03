using Application.Interfaces;
using Domain.Models.DTO;
using Domain.Models.Entities;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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

        public async Task<int> AddOrder(Order order, int quantity)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Orders.Add(order);
                    await _context.SaveChanges();
                    var item = _context.Storages.Where(a => a.BookId == order.BookId).FirstOrDefault();
                    if (item.Quantity < quantity) { throw new Exception(); }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("quantity error");
                    return -1;
                }


                return order.OrderId;
            }
        }

        public Task<List<OrderInformation>> getAllOrderInformation(string cusName)
        {
            var order = (from o in _context.Orders
                         join b in _context.Books on o.BookId equals b.Id
                         join c in _context.Customers on o.CustomerId equals c.CustomerId
                         join od in _context.OrderDetails on o.OrderId equals od.OrderId
                         where c.CustomerName == cusName
                         orderby od.OrderDetailId descending
                         select new OrderInformation()
                         {
                             OrderDetailId = od.OrderId,
                             BookName = b.Title,
                             BookId = b.Id,
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
                             BookId = b.Id,
                             BookName = b.Title,
                             CustomerName = c.CustomerName,
                             Price = b.Price,
                             Quantity = od.Quantity,
                             UnitPrice = od.UnitPrice,
                             CheckPaid = od.CheckPaid
                         }).ToList();
            return Task.FromResult(order);
        }
        public List<OrderInformation> findOrderDetailByBookId(int bookId)
        {
            var order = (from o in _context.Orders
                         join b in _context.Books on o.BookId equals b.Id
                         join c in _context.Customers on o.CustomerId equals c.CustomerId
                         join od in _context.OrderDetails on o.OrderId equals od.OrderId
                         where c.CustomerName == "customerA" && od.CheckPaid == 0 && o.BookId == bookId
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
                         }).ToList();
            return order;
            /*var orders = _context.Orders.Where(a => a.BookId == bookId).Select(a=>a.BookId).ToList();

            var orderDetail = _context.OrderDetails.Where(a => orders.Contains(a.OrderId) && a.CheckPaid == 0).ToList();

            return orderDetail;*/
        }
        public async Task<string> addQuantityOfOrderDetail(List<OrderInformation> orderDetailTest, int quantity)
        {
            foreach (var item in orderDetailTest)
            {
                var a = _context.OrderDetails.Where(a => a.OrderDetailId == item.OrderDetailId).FirstOrDefault();
                var order = _context.Orders.Where(b => b.OrderId==a.OrderId).FirstOrDefault();
                var book = _context.Books.Where(c => c.Id==order.BookId).FirstOrDefault();
                a.Quantity = a.Quantity + quantity;
                a.UnitPrice = book.Price * a.Quantity;
            }

            await _context.SaveChanges();
            return "add complete";
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

        public async Task<string> paid(List<OrderInformation> orderInformation)
        {
            foreach (var orderDetails in from item in orderInformation
                                         let order = _context.OrderDetails.Where(a => a.OrderDetailId == item.OrderDetailId).FirstOrDefault()
                                         select order)
            {
                orderDetails.CheckPaid = 1;
                var order = _context.Orders.Where(a => a.OrderId == orderDetails.OrderId).FirstOrDefault();
                var book = _context.Books.Where(a => a.Id == order.BookId).FirstOrDefault();
                var storage = _context.Storages.Where(a => a.BookId == book.Id).FirstOrDefault();
                storage.Quantity = storage.Quantity - orderDetails.Quantity;
            }
            await _context.SaveChanges();
            return "cpmpelete";
        }


    }
}
